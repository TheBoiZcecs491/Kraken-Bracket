<template>
  <v-app>
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div v-else>
      <!-- "$router.go(-1)" -->
      <v-btn @click="$router.go(-1)">&lt; BACK</v-btn>
      <h1>Signup for {{ bracket.bracketName }}</h1>
      <v-form
        @submit.prevent="formSubmit"
        ref="signUpForm"
        v-model="formValidity"
      >
        <v-container fluid>
          <v-row>
            <v-col cols="4"></v-col>
            <v-col cols="12" sm="4">
              <v-text-field
                class="email-input"
                v-model="email"
                label="Email"
                type="email"
                :rules="emailRules"
                placeholder="john@foomail.com"
                required
              >
              </v-text-field>
              <v-text-field
                class="gamertag-input"
                v-model="gamerTag"
                label="GamerTag"
                type="text"
                :rules="gamerTagRules"
                required
              ></v-text-field>
              <v-row>
                <v-col cols="12" lg="4"></v-col>
                <v-col cols="12" lg="4">
                  <!-- <router-link
                    v-show="formValidity"
                    :to="{ name: 'bracket-view', params: bracket.bracketID }"
                    class="submit-btn"
                  >
                    <v-btn @click="formSubmit" color="primary">Register!</v-btn>
                  </router-link> -->

                  <v-btn
                    v-show="formValidity"
                    @click="formSubmit"
                    color="primary"
                    >Register!</v-btn
                  >
                  <v-btn
                    class="mr-4"
                    v-if="!formValidity"
                    :disabled="!formValidity"
                    >Register!</v-btn
                  >
                  <div v-if="error">
                    <p class="red--text">{{ error }}</p>
                  </div>
                </v-col>
                <v-col cols="12" lg="4">
                  <!-- <v-btn class="mr-4" color="error" @click="resetForm"
                    >Reset Form</v-btn
                  > -->
                </v-col>
              </v-row>
            </v-col>
          </v-row>
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
      gamerTag: this.$store.state.gamerInfo.gamerTag,
      // gamerTagID: "",
      gamer: {
        gamerTag: this.gamerTag
        // gamerTagID: this.gamerTagID
      },
      error: null,
      email: this.$store.state.user.email,
      emailRules: [
        email => !!email || "Email is required",
        email =>
          email.indexOf("@") !== 0 || "Email should have a name before it",
        email => email.includes("@") || "Email should include @ symbol",
        email =>
          email.indexOf(".com") - email.indexOf("@") > 1 ||
          "Email should contain a valid domain name",
        email =>
          (email.length > 5 && email.length <= 200) || "Invalid email length"
      ],
      gamerTagRules: [
        gamerTag => !!gamerTag || "GamerTag is required",
        gamerTag =>
          (gamerTag.length >= 2 && gamerTag.length <= 20) ||
          "Invalid GamerTag length. Must be between 2 and 20 characters"
      ],
      formValidity: false
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
      if (this.$refs.signUpForm.validate()) {
        if (
          this.email == this.$store.state.user.email &&
          this.gamerTag == this.$store.state.gamerInfo.gamerTag
        ) {
          axios
            .post(
              `https://localhost:44352/api/brackets/${this.bracket.bracketID}/register/${this.gamer}`,
              {
                bracketID: this.bracket.bracketID,
                gamerTag: this.gamerTag
                // gamerTagID: this.gamerTagID
              }
            )
            .then(() => {
              setTimeout(() => {
                this.$store.dispatch("bracketPlayerInfo", this.email);
              }, 500);
            })
            .then(() => {
              this.$router.go(-1);
            });
        } else {
          this.error =
            "Either one or both of your inputs does not match your email or gamertag";
        }
      }
    },
    resetForm() {
      this.$refs.signUpForm.reset();
    },
    validateForm() {
      this.$refs.signUpForm.validate();
    }
  },
  computed: {
    ...authComputed
  }
};
</script>

<style>
.submit-btn {
  text-decoration: none;
}
</style>
