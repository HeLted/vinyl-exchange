import React, { Fragment } from "react";
import TextInput from "./../../../common/inputComponents/TextInput";
import UserThumbnail from "./../../../common/UserThumbnail";
import uuid4 from "./../../../../functions/guidGenerator";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function SaleChatComponent(props) {
  const listItems = props.data.messages.map(messageObj => {
    return (
      <li key={uuid4()}>
        <div className="alert alert-primary" role="alert">
          <h6><UserThumbnail data={{ avatar: messageObj.avatar }} /> : {messageObj.message} </h6>
        </div>
      </li>
    );
  });

  return props.data.isLoading ? (
    <BorderSpinner />
  ) : (
    <div className="chat-container border">
      <ul
        className="border border-dark"
        style={{ height: "300px", overflow: "scroll", padding: "10px" }}
      >
        {listItems}
      </ul>
      <br />
      <div className="input-group">
        <TextInput
          value={props.data.messageInput}
          id="messageInput"
          onChange={props.functions.handleOnChange}
        />
        <button
          className="btn btn-primary"
          onClick={props.functions.handleSendMessage}
        >
          Send
        </button>
      </div>
    </div>
  );
}

export default SaleChatComponent;
