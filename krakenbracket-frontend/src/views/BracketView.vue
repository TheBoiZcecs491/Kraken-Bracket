<template>
  <v-app>
    <v-container id="page-layout-fix">
      <v-btn @click="$router.go(-1)">&lt; BACK</v-btn>
      <div v-if="error" class="red--text">
        <p>404: The bracket you have been looking for is not found</p>
      </div>
      <div v-else>
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
            Number of players:
            {{ bracket.playerCount ? bracket.playerCount : 0 }}
            <span v-show="bracket.playerCount === 128">(MAX)</span>
          </h4>
          <h4>Game: {{ bracket.gamePlayed }}</h4>
          <h4>Gaming platform: {{ bracket.gamingPlatform }}</h4>
          <div>
            <h4>Rules:</h4>
            <p>{{ bracket.rules }}</p>
          </div>

          <div>
          <div v-if="bracket.maxCapacity === 2">
            <TwoPlayerBracketModel/>
          </div>
          <div v-else-if="bracket.maxCapacity === 4">
            <FourPlayerBracketModel
            :key="competitors.id"
            :competitors="competitors"
          />
          </div>
          <div v-else-if="bracket.maxCapacity === 8">
            <EightPlayerBracketModel
            :key="competitors.id"
            :competitors="competitors"
          />
          </div>
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
import TwoPlayerBracketModel from "@/components/bracket-components/TwoPlayerBracketModel.vue";
import FourPlayerBracketModel from "@/components/bracket-components/FourPlayerBracketModel.vue";
import EightPlayerBracketModel from "@/components/bracket-components/EightPlayerBracketModel.vue";

export default {
  props: ["id"],
  components: {
    UnregisterBracketModel,
    RegisterBracketModel,
    TwoPlayerBracketModel,
    FourPlayerBracketModel,
    EightPlayerBracketModel
  },
  data() {
    return {
      dialog: false,
      error: null,
      bracket: {},
      competitors: []
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
    setTimeout(BracketService.getBracketCompetitorInfo(this.id)
    .then(response => {
        this.competitors = response.data;
      })
      .catch(err => {
        // console.log(err);
        this.error = err;
      }), 1000);
    
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
    }
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
