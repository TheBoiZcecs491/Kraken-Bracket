<template>
  <v-app>
    <v-container>
      <div>
        <h1>Event View {{ event.eventName }}</h1>
        <h5>Hosted by {{ event.host }}</h5>
        <h5>Location: {{ event.address }}</h5>
        <h2>Description: {{ event.eventDescription }}</h2>
      </div>
    </v-container>
  </v-app>
</template>

<script>
import EventService from "@/services/EventService.js";
export default {
  props: ["id"],
  data() {
    return {
      event: {}
    };
  },
  created() {
    EventService.getEvents(this.id)
      .then(response => {
        this.event = response.data;
      })
      .catch(error => {
        console.log("There was an error: ", error.response);
      });
  }
};
</script>
