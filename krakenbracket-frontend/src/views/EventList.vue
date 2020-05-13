<template>
  <v-app>
    <h1>Event List</h1>
    <div v-if="loggedIn" >
      <router-link to="/event-create" class="register-btn">
        <v-btn class="primary" x-large >Create a new Event</v-btn>
      </router-link>
    </div>
    <div v-else>
      <router-link to="/login" class="register-btn">
        <v-btn class="primary" x-large >Create a new Event</v-btn>
      </router-link>
    </div>
    <div>
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
  props: {
    hearders: [],
    search_results: []
  },
  components: {
    EventCard
    // NotLoggedIn
  },
  computed: {
    ...authComputed
  },
  data() {
    return {
      events: [],
      details: "",
      filter: "",
      eventHeaders: [
        { text: "Event Name", value: "eventName" },
        { text: "Event Host", value: "Host"},
        { text: "Start Date", value: "startDate" },
        { text: "End Date", value: "endDate" },
        { text: "Location", value: "address" },
      ],
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

<style >
.register-btn{
  text-decoration: none;
}
</style>
