import React, { Component } from "react";

function SaleStatusBar(props) {
  const status = props.data.status;
  let statusColorClass = "";
  let statusBarLength = 0;

  if (status === 1) {
    statusColorClass = "bg-secondary";
  } else if (status === 2 || status === 3 || status === 4 || status === 5) {
    statusColorClass = "bg-primary";

    if (status === 2) {
      statusBarLength = 15;
    } else if (status === 3) {
      statusBarLength = 30;
    } else if (status === 4) {
      statusBarLength = 45
    } else if (status === 5) {
      statusBarLength = 60
    }
  } else if (status === 6 || status === 7) {
    statusColorClass = "bg-success";

    if(status === 6){
      statusBarLength = 75
    }else if (status ===7){
      statusBarLength = 100;
    }
  }

  return (
    <div class="progress border">
      <div
        class={
          "progress-bar progress-bar-striped progress-bar-animated " +
          statusColorClass
        }
        role="progressbar"
        style={{ width:  statusBarLength + "%" }}
      ></div>
    </div>
  );
}

export default SaleStatusBar;
