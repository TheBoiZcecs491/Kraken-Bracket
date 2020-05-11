<template>
  <v-app>
    <h1>Event List</h1>
    <div v-if="loggedIn">
      <router-link to="/event-create"
      class = "create-btn">
        <v-btn class = "primary" x-large>Create a new Event</v-btn>
      </router-link>
    </div>
    <div v-else>
      <router-link to="/login"
      class = "create-btn">
        <v-btn class = "primary" x-large>Create a new Event</v-btn>
      </router-link>
    </div>
    <div>
      <EventCard v-for="event in events" :key="event.id" :event="event" />
      <!-- <v-card>
        <v-data-table
          :headers="headers"
          :items="search_result"
          :search="filter"
          :items-per-page="10"
        >
        </v-data-table>
      </v-card> -->
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
      filter: ""
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

<style>
.create-btn {
  text-decoration: none;
}
</style>