<template>
  <v-app>
    <div v-if="loggedIn">
      <p>you are already logged in, please sign out to log into another account.</p>
    </div>
    <div v-else>
      <h1>User login</h1>
      <v-container>
        <form @submit.prevent="login">
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
              <v-btn @click="login">Login</v-btn>
              <p v-if="error" class="red--text">
                Login failed. Please try again
              </p>
              <p v-if="error" class="red--text">
                <span>{{ this.error }}</span>
              </p>
              <p>
                <router-link to="/register">register</router-link> a new
                account.
              </p>
            </v-col>
            <v-col cols="4"></v-col>
          </v-row>
        </form>
      </v-container>
      <!-- <v-btn
        v-if="$store.state.user.isLoggedIn === false"
        @click="logInUser"
        color="success"
        >Log In</v-btn
      >
      <v-btn v-else @click="logInUser" color="red text--lighten">Log out</v-btn> -->
    </div>
    <div v-if="loggedIn">
      <p>
        You are already logged in. please logout to register a new account.
      </p>
    </div>
  </v-app>
</template>

<script>
import { authComputed } from "../store/helpers.js";
export default {
  data() {
    return {
      email: "",
      password: "",
      error: null
    };
  },
  methods: {
    login() {
      // this.$store.commit("CHANGE_LOGGED_IN_STATUS");
      this.$store
        .dispatch("login", {
          email: this.email,
          password: this.password
        })
        .then(() => {
          this.$store.dispatch("bracketPlayerInfo", this.email).then(() => {
            this.$store.dispatch("gamerInfo", this.email);
          });
        })
        .then(() => {
          this.$router.go(-1);
        })
        .catch(err => {
          // console.log("****ERROR:" + err)
          this.error = err;
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
