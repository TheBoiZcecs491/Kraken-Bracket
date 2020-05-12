<template>
  <v-app>
    <div v-if="true">
      <!-- <RegisterEventModel :key="event.id" :event="event" /> -->
      <router-link
        :to="{
          name: 'event-registration',
          params: { id: event.eventID }
        }"
        class="register-btn"
      >
        <v-btn color="primary" type="submit">Register!</v-btn>
      </router-link>
    </div>
    <div v-else>
      <p><strong>NOTE:</strong> Registration is disabled; Event has ended</p>
      <v-btn disabled>Register!</v-btn>
    </div>
  </v-app>
</template>

<script>
export default {
  props: {
    event: Object
  }
};
</script>

<style>
.register-btn {
  text-decoration: none;
}
</style>
