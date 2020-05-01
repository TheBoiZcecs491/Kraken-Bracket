<template>
        <div>
          <h1>Create a new bracket</h1>
          <bracketForm />
        </div>
</template>

<script>
import bracketForm from "@/components/BracketForm.vue"
export default {
    props: ["id"],
    components: {bracketForm
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
      }
    }
  };
</script>