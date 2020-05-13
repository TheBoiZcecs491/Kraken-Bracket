<template>
  <div>
    <div v-if="!loggedIn">
      <NotLoggedIn />
    </div>
    <div v-else>
      <h1>Update Event</h1>
      <eventForm />
    </div>
  </div>
</template>

<script>
import eventForm from "@/components/EventCreateForm.vue";
import NotLoggedIn from "@/components/NotLoggedIn.vue";
import { authComputed } from "../store/helpers.js";
// import NotLoggedIn from "../components/NotLoggedIn.vue";
export default {
  props: ["id"],
  components: {
    eventForm,
    NotLoggedIn
  },
  computed: {
    ...authComputed
  },
  data() {
    return {
      EventInfo: {
        EventName: "",
        Address: "",
        EventDescription: "",
        StartDate: "",
        StartTime: "",
        Host: ""
      }
    };
  }
  // methods:{
  //   checkRegistration(){},
  //   checkHost(){}
  // }
};
</script>
