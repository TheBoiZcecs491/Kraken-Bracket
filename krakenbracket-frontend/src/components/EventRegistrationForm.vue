<template>
  <v-app>
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div v-else>
      <h1>Signup for {{ event.eventName }}</h1>
      <v-form
        @submit.prevent="submitForm"
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
                  <v-btn
                    v-show="formValidity"
                    @click="submitForm"
                    color="primary"
                    >Confirm</v-btn
                  >
                    <v-btn @click="submitForm" color="primary">Register!</v-btn>
                  </router-link> -->

                  <v-btn
                    v-show="formValidity"
                    @click="submitForm"
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
                <v-col cols="12" lg="4"> </v-col>
              </v-row>
            </v-col>
          </v-row>
        </v-container>
      </v-form>
    </div>
  </v-app>
</template>

<script>
import EventService from "@/services/EventService.js";
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
      event: {},
      gamerTag: this.$store.state.gamerInfo.gamerTag,
      gamer: {
        gamerTag: this.gamerTag
        // gamerTagID: this.gamerTagID
      },
      error: null,
      email: this.$store.state.user.email,
      emailRules: [],
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
    EventService.getEventByID(this.id)
      .then(response => {
        this.bracket = response.data;
      })
      .catch(error => {
        console.log("Error: " + error.response);
      });
    this.email = this.$store.state.user.email;
    this.gamerTag = this.$store.state.gamerInfo.gamerTag;
  },
  methods: {
    submitForm() {
      if (this.$refs.signUpForm.validate()) {
        if (
          this.email == this.$store.state.user.email &&
          this.gamerTag == this.$store.state.gamerInfo.gamerTag
        ) {
          axios
            .post(
              `https://localhost:44352/api/events/${this.event.eventID}/register/${this.$store.state.gamerInfo.hashedUserId}`,
              {
                eventID: this.event.eventID,
                hashedUserID: this.$store.state.gamerInfo.hashedUserID
              }
            )
            .then(() => {
              setTimeout(() => {
                this.$store.dispatch("eventPlayerInfo", this.id);
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
