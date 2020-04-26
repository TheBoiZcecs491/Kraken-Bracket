<template>
  <v-app>
    <div>
      <v-container>
        <form @submit.prevent="login">
          <v-row>
            <v-col cols="4"></v-col>
            <v-col cols="12" sm="4">
              <v-text-field
                class="email-input"
                label="Email"
                type="email"
                placeholder="john@foomail.com"
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
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      email: "",
      password: "",
    };
  },
  methods: {
    login() {
      // this.$store.commit("CHANGE_LOGGED_IN_STATUS");
      this.$store
        .dispatch("login", {
          email: this.email,
          password: this.password})
        // }).then(() => {
        //   this.$router.push({name: "Home"})
        // });
      this.$store.dispatch("bracketPlayerInfo", this.email)
        .then(() => {
          this.$router.go(-1);
        });
    }
  },
  created(){

  }
};
</script>
