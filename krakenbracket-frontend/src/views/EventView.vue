<template>
  <div>
    <h1>Event View {{ event.eventName }}</h1>
    <h5>Hosted by {{ event.hashedUserID }}</h5>
    <h5>Location: {{ event.address }}</h5>
    <h2>Description: {{ event.eventDescription }}</h2>
  </div>
</template>

<script>
import BracketSerive from "@/services/BracketService.js";
export default {
  props: ["id"],
  data() {
    return {
      event: {}
    };
  },
  created() {
    BracketSerive.getEvents(this.id)
      .then(response => {
        this.event = response.data;
      })
      .catch(error => {
        console.log("There was an erro: ", error.response);
      });
  }
};
</script>
