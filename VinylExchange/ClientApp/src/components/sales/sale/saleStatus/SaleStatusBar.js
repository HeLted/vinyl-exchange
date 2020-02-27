import React, { Component } from "react";

function SaleStatusBar(props) {
  const status = props.data.status;
  const statusColorClass = "";

  if (status === 1) {
      statusColorClass="bg-secondary"
  } else if (status === 2 || status ===3 || status === 4 || status === 5) {
      statusColorClass= "bg-primary"
  } else if (status === 6  || status === 7) {
    statusColorClass= "bg-success"
  } 

  return (
    <div class="progress">
      <div
        class={"progress-bar progress-bar-striped progress-bar-animated"+ statusColorClass} 
        role="progressbar"
        style={{ width: "75%" }}
      ></div>
    </div>
  );
}

export default SaleStatusBar;
