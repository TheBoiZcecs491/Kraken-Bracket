<template>
  <v-app>
    <div v-if="loggedIn">
      <router-link to="/event-create">
        <v-btn x-large>Create a new Event</v-btn>
      </router-link>
    </div>
    <div v-else>
      <router-link to="/login">
        <v-btn x-large>Create a new Event</v-btn>
      </router-link>
    </div>
    <div>
      <h1>Event List</h1>
      <EventCard v-for="event in events" :key="event.id" :event="event" />
    </div>
  </v-app>
</template>

<script>
import EventCard from "@/components/EventCard.vue";
import EventService from "@/services/EventService.js";
import { authComputed } from "../store/helpers.js";
// import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
  components: {
    EventCard,
    // NotLoggedIn
  },
  computed: {
    ...authComputed
  },
  data() {
    return {
      events: []
    };
  },
  created() {
    EventService.getEvents()
      .then(response => {
        this.events = response.data;
      })
      .catch(error => {
        console.log("Unable to get Events - error message" + error.response);
      });
  }
};
</script>
