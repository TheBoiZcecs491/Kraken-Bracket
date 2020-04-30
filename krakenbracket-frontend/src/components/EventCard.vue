<template>
  <router-link :to="{ name: 'event-view', params: { id: event.eventID } }">
    <div class="event-card">
      <h2>{{ event.eventName }}</h2>
      <h3>Start Date: {{ event.start_date }}</h3>
      <h3>End Date: {{ event.end_date }}</h3>

      <br />
      <router-link
        class="event-link"
        :to="{ name: 'event-view', params: { id: event.eventID } }"
      >
        <v-btn class="primary" color="#2196F3">View Event</v-btn>
      </router-link>
    </div>
  </router-link>
</template>

<script>
export default {
  props: {
    event: Object
  }
};
</script>

<style scoped>
.event-card {
  padding: 20px;
  padding-top: 30px;
  margin-bottom: 24px;
  transition: all 0.2s linear;
}
.event-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
.event-name {
  padding-bottom: 10px;
  font-style: italic;
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}
</style>
