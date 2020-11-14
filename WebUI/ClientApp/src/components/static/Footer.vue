<template>
  <div class="content-block">
    <footer class="footer">
      Copyright © {{new Date().getFullYear()}} {{this.$appInfo.title}} Inc.
      <br />All trademarks or registered trademarks are property of their
      respective owners.
    </footer>
  </div>
</template>

<style lang="scss">
.footer {
  @import "../../themes/generated/variables.base.scss";
  display: block;
  color: rgba($base-text-color, alpha($base-text-color) * 0.7);
  border-top: 1px solid rgba(0, 0, 0, 0.1);
  padding-top: 4px;
}

.dx-swatch-arktika-scheme-dark {
  @import "../../themes/generated/variables.additional.scss";

  .footer {
    display: block;
    color: rgba($base-text-color, alpha($base-text-color) * 0.7);
    border-top: 1px solid rgba(255, 255, 255, 0.1);
    padding-top: 4px;
  }
}
</style>
