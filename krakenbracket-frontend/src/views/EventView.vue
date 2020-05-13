<template>
  <v-app>
    <v-container>
      <v-row>
        <v-col cols="12" lg="6">
          <div>
            <h1>Event name: {{ event.eventName }}</h1>
            <h5>Hosted by {{ event.host }}</h5>
            <h5>Location: {{ event.address }}</h5>
            <h2>Description: {{ event.description }}</h2>
          </div>
          <h4>Competitors:</h4>
          <li v-for="competitor in competitors" :key="competitor.gamerTag">{{ competitor.gamerTag }}</li>
          <div v-if="statusHost() && event.statusCode != 0">
            <router-link
              :to="{
                name: 'event-update', //'update-view'
              
              }"
            >
              <v-btn color="primary">Update Event</v-btn>
            </router-link>
            <div v-if="event.statusCode != 0">
              <v-btn color="error" @click="deleteEvent">Delete Event</v-btn> 
            </div>
            <!-- manage event -->
          </div>

          <div v-else-if="statusRegistration() && event.statusCode != 0">
            <!-- unregister -->
            <UnregisterEventModel :key="event.id" :event="event" />
          </div>

          <div v-else-if="loggedIn ">
            <div v-if="event.statusCode != 0">
              <!-- <RegisterEventModel :key="event.id" :event="event" /> -->
              <router-link
                :to="{
                  name: 'event-registration',
                  params: { id }
                }"
                class="register-btn"
              >
                <v-btn color="primary" type="submit">Register!</v-btn>
              </router-link>
            </div>
            <div v-else>
              <p>
                <strong>NOTE:</strong> Registration is disabled; Event has ended
              </p>
              <v-btn disabled>Register!</v-btn>
            </div>
          </div>

          <div v-else>
            <!-- log in -->
            <br>
            <p>*Need to login to register to Event*</p>
            <router-link
              :to="{
                name: 'login-view'
              }"
            >
              <v-btn color="primary">Login</v-btn>
            </router-link>
          </div>
          
          </v-col>
          <div>
            <h1>List of Bracket in Event</h1>
            <BracketModel
              v-for="bracket in brackets"
              :key="bracket.id"
              :bracket="bracket"
            />
        </div>
        <v-col cols="12" lg="6"></v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
import EventService from "@/services/EventService.js";
import { authComputed } from "../store/helpers.js";
import UnregisterEventModel from "@/components/UnregisterEventModel.vue";
// import RegisterEventModel from "@/components/RegisterEventModel.vue";
import BracketModel from "@/components/BracketModel.vue";
import axios from "axios";

export default {
  props: ["id"],
  components: {
    UnregisterEventModel,
    // RegisterEventModel,
    BracketModel
  },
  computed: {
    ...authComputed
  },
  data() {
    return {
      event: {},
      brackets: {},
      HostGamerTag: event.host,
      competitors: []
    };
  },
  created() {
    EventService.getEventByID(this.id)
      .then(response => {
        this.event = response.data;
      })
      .catch(error => {
        console.log("There was an error: ", error.response);
      }),
      EventService.getEventHost(this.id).then(response => {
        this.HostGamerTag = response;
      });

    this.$store.dispatch("eventPlayerInfo", this.id);

    EventService.getBracketEvent(this.id).then(response => {
      this.brackets = response.data;
    });

    EventService.getEventBracketCompetitor(this.id).then(response => {
      this.competitors = response.data;
    });
  },
  beforeDestroy() {
    this.$store.dispatch("removeEventPlayerInfo");
  },
  methods: {
    statusRegistration() {
      if (!this.loggedIn) {
        return false;
      } else {
        for (
          let index = 0;
          index < this.$store.state.eventPlayerInfo.length;
          index++
        ) {
          if (
            this.$store.state.eventPlayerInfo[index].hashedUserID ===
            this.$store.state.gamerInfo.hashedUserID
          ) {
            return true;
          }
        }
      }
    },

    statusHost() {
      if (!this.loggedIn) {
        return false;
      } else {
        if (this.$store.state.gamerInfo.gamerTag == this.event.host) {
          return true;
        } else {
          return false;
        }
      }
    },

    deleteEvent() {
      if (this.event.statusCode == 1) {
        // Current
        var reason = prompt(
          "Please enter reason for deleting in-progress bracket"
        );
        this.event.reason = reason;
        var cancelledTitle = "[Cancelled] " + this.event.eventName;
        axios.put(`https://localhost:44352/api/brackets/updateEvent/`, {
          eventID: this.event.eventID,
          address: this.event.address,
          description: this.event.description,
          StartDate: this.event.startDate,
          EndDate: this.event.endDate,
          eventName: cancelledTitle,
          statusCode: 0,
          Reason: this.event.reason
        });
      } else if (this.UnregisterEventModel.statusCode == 2) {
        // future
        axios.put(`https://localhost:44352/api/events/deleteEvent/`, {
          EventID: this.event.EventID,
        });
      } else {
        // ended
        alert(
          "This Ended has already ended, further changes are not permitted."
        );
      }
    }

  }
};
</script>

<style scoped>
.standings {
  width: 50%;
  border: 3px solid black;
  text-align: center;
}
</style>