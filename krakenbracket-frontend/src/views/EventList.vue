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
      <v-card>
        <v-data-table
          :headers="eventHeader"
          :items="events"
          :search="filter"
          :items-per-page="10"
        >
        </v-data-table>
      </v-card>
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
