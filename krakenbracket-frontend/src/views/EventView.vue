<template>
  <v-app>
    <v-container>
      <div>
        <h1>Event View {{ event.EventName }}</h1>
        <h5>Hosted by {{ event.host }}</h5>
        <h5>Location: {{ event.Address }}</h5>
        <h2>Description: {{ event.eventDescription }}</h2>
      </div>
    </v-container>
    <div v-if="statusHost">
      <!-- manage event -->
      <p>host update</p>
    </div>
    <div v-else-if="statusRegistration">
      <!-- unregister -->
      <p>unregister</p>
      <UnregisterEventModel :key="event.id" :event="event" />
    </div>
    <div v-else-if="loggedIn">
      <!-- register -->
      <p>register</p>
      <RegisterEventModel :key="event.id" :event="event" />
    </div>
    <div v-else>
      <!-- log in -->
      <p>4</p>
      <router-link
            :to="{
              name: 'login-view'
            }"
          >
            <v-btn color="primary">Login</v-btn>
          </router-link>
    </div>
  </v-app>
</template>

<script>
import EventService from "@/services/EventService.js";
import { authComputed } from "../store/helpers.js";
import axios from "axios";
// import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
  props: ["id"],
  computed: {
    ...authComputed
  },
  data() {
    return {
      event: {},
      HostGamerTag: null,
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
    EventService.getEventHost(this.id)
      .then(response => {
        this.HostGamerTag = response;
      })
    
  },
  methods:{
    statusRegistration(){
      if(!this.loggedIn){
        return false;
      }else{
        axios.get(
        `https://localhost:44352/api/events/${this.event.eventID}/statusRegistration/${this.$store.state.gamerInfo.gamerTag}`,
        {
          eventID: this.event.eventID,
          gamerTag: this.$store.state.gamerInfo.gamerTag
        }
      );
      }
    },
    statusHost(){
      if(!this.loggedIn){
        return false;
      }else{
        if(this.$store.state.gamerInfo.gamerTag == this.event.host){
          return true;
        } else {
          return false;
        }
      }
    }
  }
};
</script>
