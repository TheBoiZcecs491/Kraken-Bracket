<template>
<v-app>
  <div>
    <h1>Bracket Listings</h1>
      <router-link to="/new-bracket"
      class = "create-btn">
        <v-btn class="primary" x-large>Create a new bracket</v-btn>
      </router-link>
    <!-- the :event sends each prop to the Bracket component -->
    <BracketModel
      v-for="bracket in brackets"
      :key="bracket.id"
      :bracket="bracket"
    />
  </div>
</v-app>
</template>

<script>
import BracketModel from "@/components/BracketModel.vue";
import BracketService from "@/services/BracketService.js";
export default {
  components: {
    BracketModel
  },
  data() {
    return {
      brackets: []
    };
  },
  created() {
    BracketService.getBrackets()
      .then(response => {
        this.brackets = response.data;
        //console.log(response.data)
      })
      .catch(error => {
        console.log("There was an error: " + error);
      });
  }
};
</script>

<style>
.create-btn {
  text-decoration: none;
}
</style>