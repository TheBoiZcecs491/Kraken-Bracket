<template>
  <v-app>
    <v-container id="page-layout-fix">
      <v-btn @click="$router.go(-1)">&lt; BACK</v-btn>
      <div v-if="error" class="red--text">
        <p>404: The bracket you have been looking for is not found</p>
      </div>
      <div v-else>
        <h1 id="title">{{ bracket.bracketName }}</h1>
        <h2>Host: {{ bracket.host }}</h2>
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
            Player Capacity: {{ bracket.maxCapacity ? bracket.maxCapacity : 0 }}
            <span v-show="bracket.maxCapacity === 128">(MAX)</span>
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
              <strong>NOTE:</strong> You are not currently logged in. Please
              login to register for this bracket
            </p>
            <router-link
              :to="{
                name: 'login-view'
              }"
            >
              <v-btn color="primary">Login</v-btn>
            </router-link>
          </div>
          <div
            v-else-if="loggedIn && registeredStatus(bracketPlayerInfo, bracket)"
          >
            <p>You are already registered for this event</p>
            <UnregisterBracketModel :key="bracket.id" :bracket="bracket" />
          </div>
          <!-- State if the user is logged in -->
          <div v-else>
            <RegisterBracketModel :key="bracket.id" :bracket="bracket" />
          </div>
          <div v-if="loggedIn">
            <br>
            <div v-if="((bracket.host === this.$store.state.gamerInfo.gamerTag) &&
             (bracket.statusCode == 0 || bracket.statusCode == 2))">
               <router-link
              :to="{
                name: 'bracket-update', params: {bracket: this.bracket}
              }"
            >
              <v-btn color="primary">Update Bracket</v-btn>
            </router-link>
            <v-btn color="error" @click="deleteBracket">Delete Bracket</v-btn> 
            </div>
          </div>
        </div>
      </div>
    </v-container>
  </v-app>
</template>

<script>
import BracketService from "@/services/BracketService.js";
import { authComputed } from "../store/helpers.js";
import UnregisterBracketModel from "@/components/UnregisterBracketModel.vue";
import RegisterBracketModel from "@/components/RegisterBracketModel.vue";
import axios from "axios";

export default {
  props: ["id"],
  components: {
    UnregisterBracketModel,
    RegisterBracketModel,
    // UpdateBracketView
  },
  data() {
    return {
      dialog: false,
      error: null,
      bracket: {}
    };
  },
  created() {
    BracketService.getBracketByID(this.id)
      .then(response => {
        this.bracket = response.data;
      })
      .catch(err => {
        // console.log(err);
        this.error = err;
      });
  },
  computed: {
    ...authComputed
  },
  methods: {
    registeredStatus(bracketPlayerInfo, bracket) {
      for (let index = 0; index < bracketPlayerInfo.length; index++) {
        if (bracketPlayerInfo[index].bracketID === bracket.bracketID) {
          return true;
        }
      }
    },
    deleteBracket() {
      if (this.bracket.statusCode == 2) { // If bracket is in progress
        var reason = prompt(
          "Please enter reason for deleting in-progress bracket"
        );
        this.bracket.reason = reason;
        var cancelledTitle = "[Cancelled] " + this.bracket.bracketName;
        axios.put(`https://localhost:44352/api/brackets/deleteBracket/`, {
          BracketID: this.bracket.bracketID,
          BracketName: cancelledTitle,
          Host: this.bracket.host,
          BracketTypeID: this.bracket.bracketTypeID,
          PlayerCount: this.bracket.playerCount,
          GamePlayed: this.bracket.gamePlayed,
          GamingPlatform: this.bracket.gamingPlatform,
          Rules: this.bracket.rules,
          StartDate: this.bracket.startDate,
          EndDate: this.bracket.endDate,
          StatusCode: this.bracket.statusCode,
          MaxCapacity: this.bracket.maxCapacity,
          Reason: this.bracket.reason
        })
      } 
      else if(this.bracket.statusCode == 1) // If bracket has already ended
      {
        alert("This bracket has already ended, further changes are not permitted.");
      }
      else { // If bracket has not started yet
        axios.put(`https://localhost:44352/api/brackets/deleteBracket/`, {
          BracketID: this.bracket.bracketID,
          BracketName: cancelledTitle,
          Host: this.bracket.host,
          BracketTypeID: this.bracket.bracketTypeID,
          PlayerCount: this.bracket.playerCount,
          GamePlayed: this.bracket.gamePlayed,
          GamingPlatform: this.bracket.gamingPlatform,
          Rules: this.bracket.rules,
          StartDate: this.bracket.startDate,
          EndDate: this.bracket.endDate,
          StatusCode: this.bracket.statusCode,
          MaxCapacity: this.bracket.maxCapacity,
          Reason: this.bracket.reason
        });
      }
    }
  }
}
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