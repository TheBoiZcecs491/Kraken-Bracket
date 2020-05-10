<template>
  <div>
    <v-container fluid>
      <v-row>
        <v-col cols="12" sm="1">
          <select class="search-options" v-model="keyword">
            <option v-for="option in options" v-bind:key="option.value">
              {{ option.text }}
            </option>
          </select>
        </v-col>
        <v-col cols="12" sm="4">
          <v-text-field
            class="search-input"
            v-model="search"
            type="search"
            placeholder="Search..."
            required
            clearable
          >
          </v-text-field>
        </v-col>
        <v-col cols="12" sm="1">
          <router-link
            :to="{
              name: 'search-view',
              params: { search: search, keyword: keyword }
            }"
          >
            <v-btn class="primary" color="#2196F3">Search</v-btn>
          </router-link>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "SearchBar",
  data() {
    return {
      keyword: "brackets",
      search: "",
      options: [
        { text: "Brackets", value: "bracket" },
        { text: "Events", value: "event" },
        { text: "Gamers", value: "gamer" }
      ]
    };
  }
};
</script>

<style>
.search-options {
  background: #dfe5fb;
  border: none;
  color: #394066;
  text-transform: lowercase;
  font-variant: small-caps;
}
</style>
