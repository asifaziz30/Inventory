// Write your JavaScript code.
"use strict";



var TMT = (function () {
  function TMT() {

  }
  TMT.STATUS_ENUM = {
    CREATED: 0,
    ACTIVE: 1,
    INACTIVE: 2,
    DELETED: 4,
    SUSPENDED: 5
  }

  TMT.STATUS_NAME = {
    CREATED : "Created",
    ACTIVE : "Active",
    INACTIVE : "InActive",
    DELETED : "Deleted",
    SUSPENDED : "Suspended"
  }

  TMT.GET_STATUS_BY_ID = function (statusId) {
    switch (statusId) {
      case TMT.STATUS_ENUM.CREATED:
        return TMT.STATUS_NAME.CREATED;
        break;
      case TMT.STATUS_ENUM.ACTIVE:
        return TMT.STATUS_NAME.ACTIVE;
        break;
      case TMT.STATUS_ENUM.INACTIVE:
        return TMT.STATUS_NAME.InActive;
        break;
      case TMT.STATUS_ENUM.DELETED:
        return TMT.STATUS_NAME.DELETED;
        break;
      case TMT.STATUS_ENUM.SUSPENDED:
        return TMT.STATUS_NAME.SUSPENDED;
        break;
      default:
        return statusId;
        break;
    }
  };

  const URLS = {
    USER_MANAGEMENT : {
      UserGroup :{
        INDEX: { URL: "/UserManagement/UserGroup/Index", METHOD_TYPE: "GET" },
        LOADDATA: { URL: "/UserManagement/UserGroup/LoadDataAsync", METHOD_TYPE: "POST" }
      }
    },
  };

  TMT.URLS = URLS;
  return TMT;
})();
