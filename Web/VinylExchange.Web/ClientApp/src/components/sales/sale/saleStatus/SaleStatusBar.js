import React from "react";

function SaleStatusBar(props) {
  const status = props.data.status;
  let statusColorClass = "";
  let statusBarLength = 0;

  if (status === 1) {
    statusColorClass = "bg-secondary";
  } else if (status === 2 || status === 3 || status === 4 || status === 5) {
    statusColorClass = "bg-primary";

    if (status === 2) {
      statusBarLength = 16;
    } else if (status === 3) {
      statusBarLength = 32;
    } else if (status === 4) {
      statusBarLength = 64
    } else if (status === 5) {
      statusBarLength = 80
    }
  } else if (status === 6 ) {
    statusColorClass = "bg-success";

    if(status === 6){
      statusBarLength = 100
    }
  }

  return (
    <div className="progress border">
      <div
        className={
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
