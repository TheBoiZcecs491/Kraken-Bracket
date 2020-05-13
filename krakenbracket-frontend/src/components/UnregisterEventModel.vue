<template>
  <div class="text-xs-center">
    <v-dialog v-model="dialog" width="600">
      <template v-slot:activator="{ on }">
        <v-btn color="red lighten-2" dark v-on="on">
          Unregister
        </v-btn>
      </template>

      <v-card>
        <v-card-title class="headline grey lighten-2" primary-title>
          Confirm Unregistration
        </v-card-title>
        <div style="text-align: center;">
          <v-card-text v-if="true">
            <p>
              The event has not begun yet. Are you sure you want to
              unregister?
            </p>
          </v-card-text>
          <v-card-text v-else-if="false">
            <p>
              Unregistration disabled. The event has ended
            </p>
          </v-card-text>
          <v-card-text v-else-if="false">
            <p>Event is in progress. Are you sure you want to unregister?</p>
          </v-card-text>
        </div>
        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="unregisterSubmit">
            I Accept
          </v-btn>
          <v-btn color="error" text @click="dialog = false">Cancel</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import EventService from "@/services/EventService.js";
import axios from "axios";
export default {
  props: {
    event: Object
  },
  data() {
    return {
      dialog: false
    };
  },
  created() {
    EventService.getEventByID(this.id)
      .then(response => {
        this.event = response.data;
      })
      .catch(error => {
        console.log("Error: " + error.response);
      });
  },
  methods: {
    unregisterSubmit() {
      //var systemID = this.$store.state.user.systemID;
      // var email = this.$store.state.user.email;
      axios.delete(
        `https://localhost:44352/api/events/${this.event.eventID}/unregister/${this.$store.state.gamerInfo.hashedUserID}`
      );
    }
  }
};
</script>
