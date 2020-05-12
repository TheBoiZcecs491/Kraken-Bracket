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
            Number of players:
            {{ bracket.playerCount ? bracket.playerCount : 0 }}
            <span v-show="bracket.playerCount === 128">(MAX)</span>
          </h4>
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

          <div>
            <div v-if="bracket.maxCapacity === 4">
              <FourPlayerBracketModel
                :competitors="competitors"
                :bracket="bracket"
              />
            </div>
            <div v-else-if="bracket.maxCapacity === 8">
              <EightPlayerBracketModel
                :competitors="competitors"
                :bracket="bracket"
              />
            </div>
            <div v-else-if="bracket.maxCapacity === 16">
              <SixteenPlayerBracketModel
                :competitors="competitors"
                :bracket="bracket"
              />
            </div>
            <div v-else-if="bracket.maxCapacity === 32">
              <ThirtyTwoPlayerBracketModel
                :competitors="competitors"
                :bracket="bracket"
              />
            </div>
            <div v-else-if="bracket.maxCapacity === 64">
              <SixtyFourPlayerBracketModel
                :competitors="competitors"
                :bracket="bracket"
              />
            </div>
            <div v-else-if="bracket.maxCapacity === 128">
              <OneTwentyEightPlayerBracketModel
                :competitors="competitors"
                :bracket="bracket"
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
            <div style="text-align: left">
              <!-- <RegisterBracketModel :key="bracket.id" :bracket="bracket" /> -->
              <router-link
                v-show="
                  bracket.statusCode === 0 &&
                    bracket.playerCount < bracket.maxCapacity
                "
                :to="{
                  name: 'bracket-registration',
                  params: { id: bracket.bracketID }
                }"
                class="register-btn"
              >
                <v-btn color="primary" type="submit">Register!</v-btn>
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
                v-show="
                  bracket.statusCode !== 0 ||
                    bracket.playerCount === bracket.maxCapacity
                "
                disabled
                >Register!</v-btn
              >
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
import FourPlayerBracketModel from "@/components/bracket-components/FourPlayerBracketModel.vue";
import EightPlayerBracketModel from "@/components/bracket-components/EightPlayerBracketModel.vue";
import SixteenPlayerBracketModel from "@/components/bracket-components/SixteenPlayerBracketModel.vue";
import ThirtyTwoPlayerBracketModel from "@/components/bracket-components/ThirtyTwoPlayerBracketModel.vue";
import SixtyFourPlayerBracketModel from "@/components/bracket-components/SixtyFourPlayerBracketModel.vue";
import OneTwentyEightPlayerBracketModel from "@/components/bracket-components/SixtyFourPlayerBracketModel.vue";
import axios from "axios";

export default {
  props: ["id"],
  components: {
    UnregisterBracketModel,
    FourPlayerBracketModel,
    EightPlayerBracketModel,
    SixteenPlayerBracketModel,
    ThirtyTwoPlayerBracketModel,
    SixtyFourPlayerBracketModel,
    OneTwentyEightPlayerBracketModel
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
    setTimeout(
      BracketService.getBracketCompetitorInfo(this.id)
        .then(response => {
          this.competitors = response.data;
        })
        .catch(err => {
          // console.log(err);
          this.error = err;
        }),
      1000
    );
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
      if (this.bracket.statusCode == 2) {
        // If bracket is in progress
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
        });
      } else if (this.bracket.statusCode == 1) {
        // If bracket has already ended
        alert(
          "This bracket has already ended, further changes are not permitted."
        );
      } else {
        // If bracket has not started yet
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
