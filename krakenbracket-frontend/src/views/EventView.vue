<template>
  <v-app>
    <v-container>
      <div v-if="!loggedIn">
          <p>
            <strong>NOTE:</strong> You are not currently logged in. Please login
            to register for this bracket
          </p>
        <router-link
          :to="{
            name: 'login-view'
          }"
        >
          <v-btn color="primary">Login</v-btn>
        </router-link>
      </div>
      <div>
        <h1>Event View {{ event.eventName }}</h1>
        <h5>Hosted by {{ event.hashedUserID }}</h5>
        <h5>Location: {{ event.address }}</h5>
        <h2>Description: {{ event.eventDescription }}</h2>
      </div>
    </v-container>
  </v-app>
</template>

<script>
import BracketSerive from "@/services/BracketService.js";
import { authComputed } from "../store/helpers.js";
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
