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
        <!-- 
        Status codes:
        2 - bracket is in progress
        1 - bracket not in progress and has already completed
        0 - bracket not in progress and has not begun
                 -->
        <div style="text-align: center;">
          <v-card-text v-if="true">
            <p>
              The bracket has not begun yet. Are you sure you want to
              unregister?
            </p>
          </v-card-text>
          <v-card-text v-else-if="false">
            <p>
              Unregistration disabled. The bracket has already been completed
            </p>
          </v-card-text>
          <v-card-text v-else-if="false">
            <p>Bracket is in progress. Are you sure you want to unregister?</p>
          </v-card-text>
        </div>
        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" flat @click="unregisterSubmit">
            I Accept
          </v-btn>
          <v-btn color="error" flat @click="dialog = false">Cancel</v-btn>
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
    bracket: Object
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
      var email = this.$store.state.user.email;
      axios.delete(
        `https://localhost:44352/api/events/${this.event.eventID}/unregister/${this.$store.state.user.systemID}`,
        {
          eventID: this.event.eventID,
          systemID: this.$store.state.user.systemID
        }
      );
      setTimeout(() => {
        this.$store.dispatch("bracketPlayerInfo", email);
      }, 500);
    }
  }
};
</script>
