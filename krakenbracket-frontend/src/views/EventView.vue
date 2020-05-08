<template>
  <v-app>
    <v-container>
      <div>
        <h1>Event View {{ event.EventName }}</h1>
        <h5>Hosted by {{ event.host }}</h5>
        <h5>Location: {{ event.address }}</h5>
        <h2>Description: {{ event.eventDescription }}</h2>
      </div>
      <v-row>
        <v-col cols="12" lg="12"></v-col>
        <v-col cols="12" lg="12">
          <div v-if="statusHost()">
            <router-link
              :to="{
                name: 'login-view'
              }"
            >
              <v-btn color="primary">Update</v-btn>
            </router-link>
            <!-- manage event -->
            <p>host update</p>
            <!-- <RegisterEventModel :key="event.id" :event="event" /> -->
          </div>
          <div v-else-if="loggedIn && statusRegistration()">
            <!-- unregister -->
            <p>unregister</p>
            <UnregisterEventModel :key="event.id" :event="event" />
          </div>
          <div v-else-if="loggedIn && statusRegistration()">
            <!-- register -->
            <p>register</p>
            <RegisterEventModel :key="event.id" :event="event" />
          </div>
          <div v-else>
            <!-- log in -->
            <p>4</p>
            <v-btn>Login</v-btn>
          </div>
        </v-col>
        <v-col cols="12" lg="6"></v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
import EventService from "@/services/EventService.js";
import { authComputed } from "../store/helpers.js";
import UnregisterEventModel from "@/components/UnregisterEventModel.vue";
import RegisterEventModel from "@/components/RegisterEventModel.vue";
import axios from "axios";
// import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
  props: ["id"],
  components: {
    UnregisterEventModel,
    RegisterEventModel
  },
  computed: {
    ...authComputed
  },
  data() {
    return {
      event: {},
      HostGamerTag: null
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
  },
  methods: {
    statusRegistration() {
      if (!this.loggedIn) {
        return false;
      } else {
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
