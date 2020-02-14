import React from "react";

function PlayerTrack(props) {
  return (
    <li>
      <a href={props.path}>
        {props.name}
      </a>
    </li>
  );
}

export default PlayerTrack;