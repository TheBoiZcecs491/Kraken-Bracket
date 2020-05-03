<template>
        <div>
          <h1>Create a new Event</h1>
          <eventForm />
        </div>
</template>

<script>
import eventForm from "@/components/EventCreateForm.vue"
export default {
    props: ["id"],
    components: {eventForm
  },
  data() {
      return {
          EvnetInfo: {
              EventName: "",
              Address: "",
              EventDescription: "",
              StartDate: "",
              StartTime: "",
              EndDate: "",
              EndTime: ""
          }
      }
    }
  };
</script>