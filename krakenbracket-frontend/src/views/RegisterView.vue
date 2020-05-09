<template>
  <v-app>
    <div v-if="loggedIn">
      <p>you are already logged in, please sign out to register another account.</p>
    </div>
    <div v-else>
      <h1>Register new user</h1>
      <v-container>
        <form @submit.prevent="registerUser">
          <v-row>
            <v-col cols="4"></v-col>
            <v-col cols="12" sm="4">
              <v-text-field
                class="email-input"
                label="Email"
                type="email"
                placeholder="name@example.com"
                v-model="email"
                required
              ></v-text-field>
              <v-text-field
                class="password-input"
                label="Password"
                type="password"
                v-model="password"
                required
              ></v-text-field>
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
                class="gamerTag-input"
                label="Gamer Tag"
                type="gamerTag"
                v-model="gamerTag"
                required
              ></v-text-field>
              <v-btn @click="registerUser">
                Register User
              </v-btn>
              <p v-if="error" class="red--text">
                Registration failed.<span>{{ this.errorMsg }}</span>. Please try again
              </p>
              <p v-if="error" class="red--text"><span>{{ this.error }}</span></p>
              <p>
              <router-link to="/login">Login</router-link> instead.
              </p>
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
      gamerTag: "",
      error: null,
      errorMsg: ""
    };
  },
  methods: {
    registerUser() {
      // this.$store.commit("CHANGE_LOGGED_IN_STATUS");
      //note: insertuseracc, will automatically make the salt and hash
      this.$store.dispatch("registerUser", {
        firstName: this.firstName,
        lastName: this.lastName,
        email: this.email,
        password: this.password,
        gamerTag: this.gamerTag
      }).then(() =>{
        this.$store.dispatch("login", {
        email: this.email,
        password: this.password
      }).then(() =>{
        this.$router.push("/registrationSuccess");
      });
      })
      .catch(err =>{
        // console.log("****ERROR:" + err)
        //if(err=="Error: Request failed with status code 406")
        //  this.errorMsg = "The provided Registration info is not "+
        //  "correct or the email is already in use. "+
        //  "The password could also be not secure enough";
        //else if(err=="Error: Request failed with status code 500")
        //  this.errorMsg = "The server failed to create the account.";
        //else if(err=="Error: Request failed with status code 401")
        //  this.errorMsg = "The server did not have permission to make this account.";
        this.error = err
      });
      // }).then(() => {
      //   this.$router.push({name: "Home"})
      // });
    }
  },
  computed: {
    ...authComputed
  },
  created() {}
};
</script>
