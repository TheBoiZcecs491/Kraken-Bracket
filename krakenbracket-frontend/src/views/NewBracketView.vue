<template>
  <div>
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div v-else>
      <h1>Create a new bracket</h1>
      <bracketForm />
    </div>
  </div>
</template>

<script>
import bracketForm from "@/components/BracketForm.vue";
import { authComputed } from "../store/helpers.js";
import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
  props: ["id"],
  components: {
    bracketForm,
    NotLoggedIn
  },
  data() {
    return {
      BracketInfo: {
        BracketName: "",
        PlayerCount: "",
        GamePlayed: "",
        GamePlatform: "",
        Rules: "",
        StartDate: "",
        StartTime: "",
        EndDate: "",
        EndTime: ""
      }
    };
  },
  computed: {
    ...authComputed
  }
};
</script>
