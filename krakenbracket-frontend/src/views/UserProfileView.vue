<template>
  <v-app>
    <div v-if="!loggedIn">
      <p>You must be logged in to edit profile information</p>
    </div>
    <div v-else>
      <h1>user profile</h1>
      <p>this page is under development</p>
    </div>
  </v-app>
</template>

<script>
import { authComputed } from "../store/helpers.js";
export default {
  data() {
    return {
    };
  },
  methods: {
    //noclue
  },
  computed: {
    ...authComputed
  },
  created() {}
};
</script>
