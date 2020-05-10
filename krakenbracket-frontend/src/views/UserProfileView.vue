<template>
  <v-app>
    <div v-if="!loggedIn">
      <p>You must be logged in to edit profile information</p>
    </div>
    <div v-else>
      <h1>user profile</h1>
      <p>this page is under development</p>
      <v-container>
        <form @submit.prevent="registerUser">
          <v-row>
            <v-col cols="4"></v-col>
            <v-col cols="12" sm="4">
              <v-text-field
                class="firstName-input"
                label="First Name"
                type="firstName"
                v-model="firstName"
                required
              ></v-text-field>
              <v-text-field
                class="lastName-input"
                label="Last Name"
                type="lastName"
                v-model="lastName"
                required
              ></v-text-field>
              <v-text-field
                class="new-password-input"
                label="New password"
                type="password" 
                v-model="newPassword"
                required
              ></v-text-field>
              <v-text-field
                class="retype-password-input"
                label="Retype password"
                type="password" 
                v-model="retypePassword"
                required
              ></v-text-field>
              <v-text-field
                class="current-password-input"
                label="Password"
                type="password"
                v-model="currentPassword"
                required
              ></v-text-field>
              <v-btn @click="updateUser">
                Update profile
              </v-btn>
              <p v-if="error" class="red--text">
                Failed to update user profile. Please try again
              </p>
              <p v-if="errorMsg" class="red--text"><span>{{ this.errorMsg }}</span></p>
              <p v-if="successMsg"><span>{{ this.successMsg }}</span></p>
            </v-col>
            <v-col cols="4"></v-col>
          </v-row>
        </form>
      </v-container>
    </div>
  </v-app>
</template>

<script>
import { authComputed } from "../store/helpers.js";
export default {
  data() {
    return {
      firstName: "",
      lastName: "",
      email: "",
      currentPassword: "",
      newPassword: null,
      retypePassword: null,
      error: null,
      errorMsg: null,
      successMsg: null
    };
  },
  methods: {
    updateUser() {
        if(this.newPassword!=null){
            if(this.newPassword!=this.retypePassword){
                //passwords dont match, so prevent change
                this.errorMsg = "passwords dont match";
                this.successMsg = null;
                this.firstName = this.$store.state.user.firstName;
                this.lastName = this.$store.state.user.lastName;
            }
            else{
                this.errorMsg = null;
                this.$store.dispatch("updateUser", {
        firstName: this.firstName,
        lastName: this.lastName,
        email: this.email,
        currentPassword: this.currentPassword,
        newPassword: this.newPassword
      }).then(() =>{
        this.successMsg = "update success";
        this.$store.state.user.firstName = this.firstName;
        this.$store.state.user.lastName = this.lastName;
      })
      .catch(err =>{
        this.error = err;
        this.successMsg = null;
        this.firstName = this.$store.state.user.firstName;
        this.lastName = this.$store.state.user.lastName;
      });
            }
        }
        else{
            this.$store.dispatch("updateUser", {
        firstName: this.firstName,
        lastName: this.lastName,
        email: this.email,
        currentPassword: this.currentPassword,
        newPassword: null
      }).then(() =>{
        this.successMsg = "update success";
        this.$store.state.user.firstName = this.firstName;
        this.$store.state.user.lastName = this.lastName;
      })
      .catch(err =>{
        this.error = err;
        this.successMsg = null;
        this.firstName = this.$store.state.user.firstName;
        this.lastName = this.$store.state.user.lastName;
      });
        }
        this.currentPassword = "";
        this.newPassword = null;
        this.retypePassword = null;
    }
  },
  computed: {
    ...authComputed
  },
  created() {
      //grab profile infos.
      this.firstName = this.$store.state.user.firstName;
      this.lastName = this.$store.state.user.lastName;
      this.email = this.$store.state.user.email;
  }
};
</script>
