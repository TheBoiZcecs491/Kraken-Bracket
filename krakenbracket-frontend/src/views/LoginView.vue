<template>
  <v-app>
    <div>
      <v-btn
        v-if="$store.state.user.isLoggedIn === false"
        @click="logInUser"
        color="success"
        >Log In</v-btn
      >
      <v-btn v-else @click="logInUser" color="red text--lighten">Log out</v-btn>
    </div>
  </v-app>
</template>

<script>
export default {
  methods: {
    logInUser() {
      this.$store.commit("CHANGE_LOGGED_IN_STATUS");
    }
  }
};
</script>
