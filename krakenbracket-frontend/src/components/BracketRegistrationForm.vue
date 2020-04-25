<template>
  <v-app>
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div v-else>
      <h1>Signup for {{ bracket.bracketName }}</h1>
      <v-form @submit.prevent="formSubmit">
        <v-container fluid>
          <v-layout row>
            <v-flex xs4>
              <!-- This element's content is intentionally empty -->
            </v-flex>
            <v-flex xs4>
              <v-text-field
                class="gamertag-input"
                v-model="gamerTag"
                label="GamerTag"
                type="text"
                required
              ></v-text-field>
              <v-text-field
                class="gamertag-id-input"
                v-model="gamerTagID"
                label="ID"
                type="text"
                placeholder="9999"
                required
              ></v-text-field>
              <router-link :to="{name: 'bracket-view', params: {id: bracket.bracketID}}">
                <v-btn @click="formSubmit" type="submit" color="primary">Register!</v-btn>
              </router-link>
              
            </v-flex>
            <v-flex xs4>
              <!-- This element's content is intentionally empty -->
            </v-flex>
          </v-layout>
        </v-container>
      </v-form>
    </div>
  </v-app>
</template>

<script>
import BracketService from "@/services/BracketService.js";
import axios from "axios";
import { authComputed } from "../store/helpers.js";
import NotLoggedIn from "../components/NotLoggedIn.vue";

export default {
  props: ["id"],
  components: {
    NotLoggedIn
  },
  data() {
    return {
      bracket: {},
      gamer: {
        gamerTag: "",
        gamerTagID: ""
      }
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
  methods: {
    formSubmit() {
      BracketService.getBracketByID(this.id).then(response => {
        console.log("data", response);
        this.bracket = response.data;
      });
      axios.post(
        `https://localhost:44352/api/brackets/${this.bracket.bracketID}/register/${this.gamer}`,
        {
          bracketID: this.bracket.bracketID,
          gamerTag: this.gamerTag,
          gamerTagID: this.gamerTagID
        }
      );
      // this.$store.dispatch("bracketPlayerInfo", this.email);
    }
  },
  computed: {
    ...authComputed
  }
};
// data: () => ({
//   emailRules: [
//     email => !!email || "Email is required",
//     email => email.indexOf("@") !== 0 || "Email should have a name before it",
//     email => email.includes("@") || "Email should include @ symbol",
//     email =>
//       email.indexOf(".") - email.indexOf("@") > 1 ||
//       "Email should contain a valid domain name",
//     email =>
//       (email.length > 5 && email.length <= 200) || "Invalid email length"
//   ],
//   gamerTagRules: [
//     gamerTag => !!gamerTag || "GamerTag is required",
//     gamerTag => gamerTag.includes("#") || "GamerTag must have # symbol",
//     gamerTag =>
//       gamerTag.indexOf("#") !== 0 ||
//       "Must have your Username before the # symbol",
//     gamerTag =>
//       (gamerTag.length >= 2 && gamerTag.length <= 20) ||
//       "Invalid GamerTag length. Must be between 2 and 20 characters"
//   ]
// })
</script>
