<template>
  <v-app>
    <div>
      <!-- <RegisterBracketModel :key="bracket.id" :bracket="bracket" /> -->
      <router-link
        :to="{
          name: 'event-registration',
          params: { id: event.eventID }
        }"
        class="register-btn"
      >
        <v-btn color="primary" type="submit">Register!</v-btn>
      </router-link>
      <div v-if="false">
        <p>
          <strong>NOTE:</strong> Registration is disabled; Event has already
          completed
        </p>
        <v-btn disabled>Register!</v-btn>
      </div>
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
