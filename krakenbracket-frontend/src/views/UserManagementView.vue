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
              
              <!-- SINGLE CREATE USERS -->
            <h4>Single Create</h4>
            <!-- System ID -->
            <v-text-field
              placeholder="System ID"
              v-model="createSystemID"
              type="number"
            ></v-text-field>
            <!-- Email -->
            <v-text-field
              placeholder="Password"
              v-model="createPassword"
            ></v-text-field>
            <!-- Account Type -->
            <v-text-field
              placeholder="Account Type"
              v-model="createAccountType"
            ></v-text-field>
            <v-btn @click="singleCreateFormSubmit">Send</v-btn>
          </v-col>
          <v-col cols="4">
              <!-- SINGLE DELETE USERS -->
            <h4>Single Delete</h4>
            <v-text-field
              placeholder="System ID"
              v-model="systemID"
              type="number"
            ></v-text-field>
            <v-text-field
              placeholder="Account Type"
              v-model="accountType"
            ></v-text-field>
            <v-btn @click="singleDeleteFormSubmit">Send</v-btn>
          </v-col>
          <v-col cols="2"></v-col>
        </v-row>
        <v-row>
          <v-col cols="2"></v-col>
          <v-col cols="4">
              
              <!-- SINGLE CREATE USERS -->
            <h4>Bulk Create</h4>
            <input type="file" @change="loadTextFromFile" id="file-input">
            <h4>Bulk Delete</h4>
            <input type="file">
            <h4>Bulk Update</h4>
            <input type="file">
           
          </v-col>
          <v-col cols="4">
           <h4>Single Update</h4>
            <v-text-field
              placeholder="System ID"
              v-model="updateSystemID"
              type="number"
            ></v-text-field>
            <v-text-field
              placeholder="First Name"
              v-model="updateFirstName"
            ></v-text-field>
             <v-text-field
              placeholder="Last Name"
              v-model="updateLastName"
            ></v-text-field>
            <v-text-field
              placeholder="Email"
              v-model="email"
              type="email"
            ></v-text-field>
            <v-text-field
              placeholder="Account Type"
              v-model="updateAccountType"
            ></v-text-field>
            <v-text-field
              placeholder="Account Status"
              v-model="updateAccountStatus"
            ></v-text-field>
            
            <v-btn @click="singleDeleteFormSubmit">Send</v-btn>
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
          createSystemID: null,
          createPassword: null,
          createAccountType: null,
          deleteSystemID: null,
          deleteAccountType: null,
          updateSystemID: null,
          firstName: null,
          lastName: null,
          email: null,
          updateAccountType: null,
          updateAccountStatus: null,
          user: {},
          csvFile: null
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
          UserManagementService.singleCreateUser(user, this.$store.state.user.accountType)
          .then(() =>{
              console.log("SUCCESS")
          })
          .catch(err =>{
              console.log(err)
          });
      },
      singleDeleteFormSubmit(){
          var user = {
              systemID: this.systemID,
              accountType: this.accountType
            }
          UserManagementService.singleDeleteUser(user, this.$store.state.user.accountType)
          .then(() =>{
              console.log("SUCCESS")
          })
          .catch(err =>{
              console.log(err)
          });
      },
      singleUpdateFormSubmit(){
          var user = {
              firstName: this.firstName,
              lastName: this.lastName,
              email: this.email,
              updateAccountType: this.updateAccountType,
              updateAccountStatus: this.updateAccountStatus,
            }
          UserManagementService.singleUpdateUser(user, this.$store.state.user.accountType)
          .then(() =>{
              console.log("SUCCESS")
          })
          .catch(err =>{
              console.log(err)
          });
      },
      loadTextFromFile() {
          console.log(document.getElementById("file-input").files); // list of File objects

        var file = document.getElementById("file-input").files[0];
        var reader = new FileReader();
        var content = reader.readAsText(file);
        console.log(content);

        // const file = ev.target.files[0];
        // const reader = new FileReader();

        // reader.onload = e => this.$emit("load", e.target.result);
        // reader.readAsText(file);
        // console.log(file)
    },
  }
};
</script>
