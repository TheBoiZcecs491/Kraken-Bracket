<template>
  <v-app>
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div
      v-else-if="
        loggedIn &&
          (this.$store.state.user.accountType === 'System Admin' ||
            this.$store.state.user.accountType === 'Admin')
      "
    >
      <h1>User Management</h1>
      <v-container>
        <v-row>
          <v-col cols="2"></v-col>
          <v-col cols="4">
            <h4>Single Create</h4>
            <!-- System ID -->
            <v-text-field
              placeholder="System ID"
              v-model="systemID"
              type="number"
            ></v-text-field>
            <!-- Email -->
            <v-text-field
              placeholder="Password"
              v-model="password"
            ></v-text-field>
            <!-- Account Type -->
            <v-text-field
              placeholder="Account Type"
              v-model="accountType"
            ></v-text-field>
            <v-btn @click="singleCreateFormSubmit">Send</v-btn>
          </v-col>
          <v-col cols="4">
            <h4>Single Delete</h4>
          </v-col>
          <v-col cols="2"></v-col>
        </v-row>
      </v-container>
    </div>
  </v-app>
</template>

<script>
import { authComputed } from "../store/helpers.js";
import NotLoggedIn from "../components/NotLoggedIn.vue";
import UserManagementService from "@/services/UserManagementService.js";

export default {
  data(){
      return{
          systemID: null,
          password: null,
          accountType: null,
          user: {}
      }
  },
  components: {
    NotLoggedIn
  },
  computed: {
    ...authComputed
  },
  methods:{
      singleCreateFormSubmit(){
          var user = {
              systemID: this.systemID,
              password: this.password,
              accountType: this.accountType
            }
          UserManagementService.singleCreateUser(user)
          .then(() =>{
              console.log("SUCCESS")
          })
          .catch(err =>{
              console.log(err)
          });
      }
  }
};
</script>
