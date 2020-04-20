<template>
  <v-app>
    <div>
      <h1 id="title">{{ bracket.bracketName }}</h1>
      <h3>Start Date: {{ bracket.startDate }}</h3>
      <h3>End Date: {{ bracket.endDate }}</h3>
      <h4>
        Number of players: {{ bracket.playerCount ? bracket.playerCount : 0 }}
      </h4>
      <h4>Game: {{ bracket.gamePlayed }}</h4>
      <h4>Gaming platform: {{ bracket.gamingPlatform }}</h4>
      <div>
        <h4>Rules:</h4>
        <p>{{ bracket.rules }}</p>
      </div>
      <router-link v-if="$store.state.user.isLoggedIn === false"
        :to="{
          name: 'login-view'
        }"
      >
        <v-btn>Register!</v-btn>
      </router-link>
      <router-link v-else
        :to="{
          name: 'bracket-registration',
          params: { id: bracket.bracketID }
        }"
      >
        <v-btn
          v-show="bracket.statusCode === 0 && bracket.playerCount < 128"
          type="submit"
          >Register!</v-btn
        >
      </router-link>
      <v-btn
        v-show="bracket.statusCode !== 0 || bracket.playerCount === 128"
        disabled
        >Register!</v-btn
      >
    </div>
  </v-app>
</template>

<script>
import BracketService from "@/services/BracketService.js";

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
  }
};
</script>

<style scoped>
#title {
  font-size: 75px;
  font-weight: 800;
}
</style>
