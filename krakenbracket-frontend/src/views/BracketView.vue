<template>
  <v-app>
    <v-container id="page-layout-fix">
      <h1 id="title">{{ bracket.bracketName }}</h1>
      <div style="text-align: left">
        <v-row>
          <v-col cols="12" lg="6">
            <h3 class="dates">Start Date: {{ bracket.startDate }}</h3>
          </v-col>
          <v-col cols="12" lg="6">
            <h3 class="dates">End Date: {{ bracket.endDate }}</h3>
          </v-col>
        </v-row>
        <h4>
          Number of players: {{ bracket.playerCount ? bracket.playerCount : 0 }}
          <span v-show="bracket.playerCount === 128">(MAX)</span>
        </h4>
        <h4>Game: {{ bracket.gamePlayed }}</h4>
        <h4>Gaming platform: {{ bracket.gamingPlatform }}</h4>
        <div>
          <h4>Rules:</h4>
          <p>{{ bracket.rules }}</p>
        </div>
        <!-- State if the user is not logged in -->
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
        <!-- State if the user is logged in -->
        <div v-else>
          <router-link
            :to="{
              name: 'bracket-registration',
              params: { id: bracket.bracketID }
            }"
            class="register-btn"
          >
            <v-btn
              v-show="bracket.statusCode === 0 && bracket.playerCount < 128"
              type="submit"
              >Register!</v-btn
            >
          </router-link>
          <div v-if="bracket.statusCode === 1">
            <p>
              <strong>NOTE:</strong> Registration is disabled; bracket has
              already completed
            </p>
          </div>
          <div v-else-if="bracket.statusCode === 2">
            <p>
              <strong>NOTE:</strong> Registration is disabled; bracket is in
              progress
            </p>
          </div>
          <v-btn
            v-show="bracket.statusCode !== 0 || bracket.playerCount === 128"
            disabled
            >Register!</v-btn
          >
        </div>
      </div>
    </v-container>
  </v-app>
</template>

<script>
import BracketService from "@/services/BracketService.js";
import {authComputed} from '../store/helpers.js'
export default {
  props: ["id"],
  data() {
    return {
      bracket: {}
    };
  },
  created() {
    BracketService.getBracketByID(this.id)
      .then(response => {
        this.bracket = response.data;
      })
      .catch(error => {
        console.log("Error: " + error.response);
      });
  },
  computed: {
    ...authComputed
  }
};
</script>

<style scoped>
#title {
  font-size: 50px;
  font-weight: 800;
}
#page-layout-fix {
  padding-left: 10%;
  padding-right: 10%;
}
.register-btn {
  text-decoration: none;
}
.dates {
  padding-top: 1em;
  padding-bottom: 0.5em;
  font-size: 25px;
}
</style>
