import React from "react";
import "./Profile.css";
import AddressManagerModalContainer from "./userModals/addressManager/AddressManagerModalContainer";
import ChangeAvatarModalContainer from "./userModals/changeAvatar/ChangeAvatarModalContainer";
import UserAvatarContainer from "./userAvatar/UserAvatarContainer";
import UserPurchasesContainer from "./userPurchases/UserPurchasesContainer";
import UserSalesContainer from "./userSales/UserSalesContainer";
import ConfirmEmailModalContainer from "./userModals/confirmEmail/ConfirmEmailModalContainer";
import PageSpinner from "./../../common/spinners/PageSpinner";
import ChangeEmailModalContainer from "./userModals/changeEmail/ChageEmailModalContainer";

function ProfileComponent(props) {
  return props.data.isLoading ? (
    <PageSpinner />
  ) : (
    <div className="container-fluid">
      <div className="row justify-content-center">
        <div className="profile-avatar-container col-3 border p-1">
          <UserAvatarContainer
            data={{ shouldAvatarUpdate: props.data.shouldAvatarUpdate }}
          />
        </div>
      </div>
      <br />

      <div className="accordion filter-collapse" id="filterCollapse">
        <div className="card">
          <div className="card-header" id="headingOne">
            <h2 className="mb-0">
              <button
                className="filter-collapse-btn btn btn-outline-primary"
                type="button"
                data-toggle="collapse"
                data-target="#collapseOne"
                aria-expanded="true"
                aria-controls="collapseOne"
              >
                User Menu
              </button>
            </h2>
          </div>

          <div
            id="collapseOne"
            className="collapse"
            aria-labelledby="headingOne"
            data-parent="#filterCollapse"
          >
            <div className="card-body">
              <div className="  row text-center justify-content-center">
                <div className="col-12">
                  <button
                    className="profile-menu-button btn btn-outline-primary btn-lg"
                    data-toggle="modal"
                    data-target="#changeAvatarModal"
                  >
                    Change Avatar
                  </button>
                  <ChangeAvatarModalContainer
                    functions={{
                      handleShouldAvatarUpdate:
                        props.functions.handleShouldAvatarUpdate
                    }}
                  />
                </div>

                <div className="col-12">
                  <button
                    className="profile-menu-button btn btn-outline-primary btn-lg"
                    data-toggle="modal"
                    data-target="#addressManagerModal"
                  >
                    Address Manager
                  </button>
                  <AddressManagerModalContainer />
                </div>
                <br />
                {props.data.user.email_verified === "false" && (
                  <div className="col-12">
                    <button
                      className="profile-menu-button btn btn-outline-primary btn-lg"
                      data-toggle="modal"
                      data-target="#confirmEmailModal"
                    >
                      Confirm Email
                    </button>
                    <ConfirmEmailModalContainer />
                  </div>
                )}
              </div>

              <div className="row justify-content-center  text-center">
                <div className=" col-12">
                  <button
                    className="profile-menu-button btn btn-outline-primary btn-lg"
                    data-toggle="modal"
                    data-target="#changeEmailModal"
                  >
                    Change Email
                  </button>
                  <ChangeEmailModalContainer />
                  <br />
                </div>

                <div className=" col-12">
                  <button className="profile-menu-button btn btn-outline-primary btn-lg">
                    Change Password
                  </button>
                  <br />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <br />

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
