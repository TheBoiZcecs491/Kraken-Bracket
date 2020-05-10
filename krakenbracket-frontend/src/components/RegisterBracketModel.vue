<template>
  <v-app>
    <div style="text-align: left">
      <!-- <RegisterBracketModel :key="bracket.id" :bracket="bracket" /> -->
      <router-link
        v-show="
          bracket.statusCode === 0 && bracket.playerCount < bracket.maxCapacity
        "
        :to="{
          name: 'bracket-registration',
          params: { id: bracket.bracketID }
        }"
        class="register-btn"
      >
        <v-btn color="primary" type="submit">Register!</v-btn>
      </router-link>
      <div v-if="bracket.statusCode === 1">
        <p>
          <strong>NOTE:</strong> Registration is disabled; bracket has already
          completed
        </p>
      </div>
      <div v-else-if="bracket.statusCode === 2">
        <p>
          <strong>NOTE:</strong> Registration is disabled; bracket is in
          progress
        </p>
      </div>
      <v-btn
        v-show="
          bracket.statusCode !== 0 ||
            bracket.playerCount === bracket.maxCapacity
        "
        disabled
        >Register!</v-btn
      >
    </div>
  </v-app>
</template>

<script>
export default {
  props: {
    bracket: Object
  }
};
</script>

<style>
.register-btn {
  text-decoration: none;
}
</style>
