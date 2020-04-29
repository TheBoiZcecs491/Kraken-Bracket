<template>
        <div>
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
              CompetitorCount: "",
              GamePlayed: "",
              GamePlatform: "",
              Rules: "",
              StartDate: "",
              StartTime: "",
              EndDate: "",
              EndTime: ""
          }
      }
    },
  };
</script>