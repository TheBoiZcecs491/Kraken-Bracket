<template>
  <v-app>
    <v-container>
      <div>
        <h1>Event name: {{ event.eventName }}</h1>
        <h5>Hosted by {{ event.host }}</h5>
        <h5>Location: {{ event.address }}</h5>
        <h2>Description: {{ event.description }}</h2>
      </div>
      <v-row>
        <v-col cols="12" lg="12"></v-col>
        <v-col cols="12" lg="12">
          <div v-if="statusHost()">
            <router-link
              :to="{
                name: 'event-update', //'update-view'
                params: { id: event.eventID }
              }"
            >
              <v-btn color="primary">Update</v-btn>
            </router-link>
            <!-- manage event -->
            <p>host update</p>
            <!-- <RegisterEventModel :key="event.id" :event="event" /> -->
          </div>

          <div v-else-if="statusRegistration()">
            <!-- unregister -->
            <p>unregister</p>
            <UnregisterEventModel :key="event.id" :event="event" />
          </div>

          <div v-else-if="loggedIn">
            <div v-if="true">
              <!-- <RegisterEventModel :key="event.id" :event="event" /> -->
              <router-link
                :to="{
                  name: 'event-registration',
                  params: { id: event.eventID }
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
            <router-link
              :to="{
                name: 'login-view'
              }"
            >
              <v-btn color="primary">Login</v-btn>
            </router-link>
          </div>
        </v-col>
        <v-col cols="12" lg="6"></v-col>
      </v-row>
      <div>
        <h1>List of Bracket in Event</h1>
        <BracketModel
          v-for="bracket in brackets"
          :key="bracket.id"
          :bracket="bracket"
        />
      </div>
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
        axios.get(
          `https://localhost:44352/api/events/${this.event.eventID}/statusRegistration/${this.$store.state.gamerInfo.gamerTag}`,
          {
            eventID: this.event.eventID,
            gamerTag: this.$store.state.gamerInfo.gamerTag
          }
        );
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
    }
  }
};
</script>
