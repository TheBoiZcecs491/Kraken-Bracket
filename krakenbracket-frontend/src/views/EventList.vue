<template>
  <div>
    <h1>Event List</h1>
    <EventCard 
      v-for="event in events" 
      :key="event.id" 
      :event="event" 
    />
  </div>
</template>

<script>
import EventCard from "@/components/EventCard.vue";
import BracketService from "@/services/BracketService.js";

export default {
  components: {
    EventCard
  },
  data() {
    return {
      events: []
    };
  },
  created() {
    BracketService.getEvents()
      .then(response => {
        this.events = response.data;
      })
      .catch(error => {
        console.log("There was an errot: " + error.response);
      });
  }
};
</script>
