import React from "react";
import "./Profile.css";
import AddressManagerModalContainer from "./userModals/addressManager/AddressManagerModalContainer";
import ChangeAvatarModalContainer from "./userModals/changeAvatar/ChangeAvatarModalContainer";
import UserAvatarContainer from "./userAvatar/UserAvatarContainer";
import UserPurchasesContainer from "./userPurchases/UserPurchasesContainer";
import UserSalesContainer from "./userSales/UserSalesContainer";

function ProfileComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row justify-content-center">
        <div className="profile-avatar-container col-3 border">
          <UserAvatarContainer
            data={{ shouldAvatarUpdate: props.data.shouldAvatarUpdate }}
          />
        </div>
      </div>
      <br />
      <div className=" profile-menu-container row border justify-content-center">
        <div className="col-2">
          <button
            className="profile-menu-button btn btn-outline-primary btn-lg"
            data-toggle="modal"
            data-target="#changeAvatarModal"
          >
            Change Avatar
          </button>
          <ChangeAvatarModalContainer
            functions={{
              handleShouldAvatarUpdate: props.functions.handleShouldAvatarUpdate
            }}
          />
        </div>

        <div className="col-2">
          <button
            className="profile-menu-button btn btn-outline-primary btn-lg"
            data-toggle="modal"
            data-target="#addressManagerModal"
          >
            Address Manager
          </button>
          <AddressManagerModalContainer />
        </div>
        <div className="col-2">
          <button className="profile-menu-button btn btn-outline-primary btn-lg">
            Confirm Email
          </button>
        </div>
      </div>
      <br />
      <div className="row border  profile-menu-container justify-content-center">
        <div className=" col-2">
          <button className="profile-menu-button btn btn-outline-primary btn-lg">
            Change Email
          </button>
        </div>
        <div className=" col-2">
          <button className="profile-menu-button btn btn-outline-primary btn-lg">
            Change Password
          </button>
        </div>
        <div className=" col-2">
          <button className="profile-menu-button btn btn-outline-primary btn-lg">
            Administration Panel
          </button>
        </div>
      </div>
      <br />
      <div className="row ">
        <div className="col-12 border p-0" style={{ marginBottom: "30px" }}>
          <div className="row border justify-content-center m-0">
            <h4 className="property-text">Purchases</h4>
          </div>
          <UserPurchasesContainer />
        </div>

        <div className="col-12 border p-0">
          <div className="row border justify-content-center m-0">
            <h4 className="property-text">Sales</h4>
          </div>
          <UserSalesContainer />
        </div>
      </div>
    </div>
  );
}

export default ProfileComponent;
