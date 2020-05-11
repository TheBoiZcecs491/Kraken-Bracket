<template>
  <v-app>
    <div v-if="!loggedIn">
      <p>You must be logged in to edit profile information</p>
    </div>
    <div v-else>
      <h1>Edit user profile</h1>
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
                class="password-input"
                label="Password"
                type="password"
                v-model="password"
                required
              ></v-text-field>
              Account enabled
              <input type="checkbox" id="checkbox" v-model="accountStatus">
              <p></p>
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
      password: "",
      newPassword: null,
      retypePassword: null,
      accountStatus: true,
      error: null,
      errorMsg: null,
      successMsg: null
    };
  },
  methods: {
    updateUser() {
        this.successMsg = null;
        this.error = null;
        this.errorMsg = null;
        if(this.newPassword!=null){
            if(this.newPassword!=this.retypePassword){
                //passwords dont match, so prevent change
                this.errorMsg = "passwords dont match";
                this.firstName = this.$store.state.user.firstName;
                this.lastName = this.$store.state.user.lastName;
                this.password = "";
                this.newPassword = null;
                this.retypePassword = null;
                this.accountStatus = this.$store.state.user.accountStatus;
            }
            else{
                this.$store.dispatch("updateUser", {
                firstName: this.firstName,
                lastName: this.lastName,
                email: this.email,
                password: this.password,
                newPassword: this.newPassword,
                accountStatus: this.accountStatus
            }).then(() =>{
                this.$store.dispatch("login", {
                    email: this.email,
                    password: this.newPassword
                })
      .then(() =>{
        this.$store.dispatch("bracketPlayerInfo", this.email).then(() => {
        this.$store.dispatch("gamerInfo", this.email)
      })
      });
                this.successMsg = "update success";
                this.password = "";
                this.newPassword = null;
                this.retypePassword = null;
            })
            .catch(err =>{
                this.error = err;
                this.firstName = this.$store.state.user.firstName;
                this.lastName = this.$store.state.user.lastName;
                this.password = "";
                this.newPassword = null;
                this.retypePassword = null;
                this.accountStatus = this.$store.state.user.accountStatus;
            });
            }

        }
        else{
            this.$store.dispatch("updateUser", {
                firstName: this.firstName,
                lastName: this.lastName,
                email: this.email,
                password: this.password,
                newPassword: null,
                accountStatus: this.accountStatus
            }).then(() =>{
                this.$store.dispatch("login", {
                    email: this.email,
                    password: this.password
                })
      .then(() =>{
        this.$store.dispatch("bracketPlayerInfo", this.email).then(() => {
        this.$store.dispatch("gamerInfo", this.email)
      })
      });
                this.successMsg = "update success";
                this.password = "";
                this.newPassword = null;
                this.retypePassword = null;
            })
            .catch(err =>{
                this.error = err;
                this.firstName = this.$store.state.user.firstName;
                this.lastName = this.$store.state.user.lastName;
                this.password = "";
                this.newPassword = null;
                this.retypePassword = null;
                this.accountStatus = this.$store.state.user.accountStatus;
            });
        }
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
