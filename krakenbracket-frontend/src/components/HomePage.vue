<template>
  <div id="app">
    <v-app id="inspire">
    <v-container fluid>
        <v-row justify="space-around">
          <v-col cols="8">
            <div class="display-1">Welcome to Kraken Bracket</div>
            <div class="subheading">Your one stop tournament</div>
          </v-col>
        </v-row>
    </v-container>
    </v-app>
  </div>
</template>

<script>
export default {
  name: "HomePage",
  props: {
    msg: String
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
