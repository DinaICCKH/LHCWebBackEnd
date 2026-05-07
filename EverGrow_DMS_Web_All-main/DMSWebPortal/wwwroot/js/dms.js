"use strict";
$(document).ready(function () {
    hideLoading();
    flatpickr(".datetime", {
        dateFormat: "d-m-Y",
    });
});
var baseUrl = "";
var UserData = {};
var Expired = false;
var promotionRun = false;
const NoProfile = "https://painrehabproducts.com/wp-content/uploads/2014/10/facebook-default-no-profile-pic.jpg";
const attachment_folder = "Attachments";
const profile_folder = "Profiles";
var profile_url = baseUrl+ ("/" + attachment_folder + "/" + profile_folder + "/");
var formdata = new FormData();
function GoHomePage() {
    window.location.href =baseUrl+ "/dms/home";
}
function GoLoginPage() {
    window.location.href = baseUrl+ "/dms/login";
}
function Logout() {
    localStorage.removeItem("UserData");
    GoLoginPage();
}
function SetExpired(key,expiryInMinutes) {
    const now = new Date();
    const expiretime = now.getTime() + expiryInMinutes * 60 * 1000;
    localStorage.setItem(key, expiretime);
}
function GetExpired(key) {
    const expiretime = localStorage.getItem(key);
    const now = new Date();
    if (now.getTime() > expiretime) {
        localStorage.removeItem(key);
        Expired = true;
    }
    else {
        Expired = false;
    }
    return Expired;
}
function CheckExpired(key) {
    if (GetExpired(key) === true) {
        Logout();
    }
}
function SetDefaultAfterLogin() {
    $(".btn-profile .user-name").html(`${UserData.UserName}`);
  /*  $(".btn-company .company-name").text(UserData.CompanyName);*/
    var defaultProfile = "/images/default-profile.png"; // set your default image path
    var profileImg = $(".profile-box .user-image");

    profileImg.attr("src", profile_url + UserData.UserProfile)
        .on("error", function () {
            $(this).attr("src", NoProfile);
        });
}
function GetItemMasterDataList(pageNumber) {
    var limit = $(".txt-limit").val();
    //alert(limit);
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = "";

    callApi({
        url: baseUrl+`/dms/get-item-master-data?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: true,
        beforeSend: function () {
            //showLoading();
        },
        complete: function () {
            //hideLoading();
        },
        success: function (data) {
            $(".table-item tbody").empty();
            let tr = "";
            for (var x of data.data) {
                tr += `<tr>
                    <td>${x.RowNumber}</td>
                    <td>${x.ItemCode}</td>
                    <td>${x.ItemName}</td>
                    <td>${x.ItemGroupName}</td>
                    <td>${x.InvUoMCode ?? "NON"}</td>
                     <td>
                        <div class="badge bg-${x.Status == 'Active' ? 'success' : 'danger'}" role="alert">
                            ${x.Status ?? "NON"}
                        </div>
                    </td>

                </tr>`;
            }
            $(".table-item tbody").append(tr);

            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetItemMasterDataList);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}

function GetBPMasterDataList(pageNumber) {
    var limit = $(".txt-limit").val();
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = "";
    callApi({
        url: baseUrl+`/dms/get-bp-master-data?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: true,
        beforeSend: function () {
            //showLoading();
        },
        complete: function () {
            //hideLoading();
        },
        success: function (data) {
            $(".table-item tbody").empty();
            $(".table-bp tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td>${x.CardCode}</td>
                                <td>${x.CardName}</td>
                                <td>${x.CardFName ?? ""}</td>
                                <td>${x.GroupName}</td>
                                <td>${x.FullAddEN ?? ""}</td>
                                <td>${x.Phone1 ?? ""}</td>
                                <td>${x.LateLong ?? ""}</td>
                                 <td>
                                    <div class="badge bg-${x.Status == 'Active' ? 'success' : 'danger'}" role="alert">
                                        ${x.Status ?? "NON"}
                                    </div>
                                </td>

                            </tr>`;
            }
            $(".table-bp tbody").append(tr);

            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetBPMasterDataList);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}

function GetSaleEmlpoyeeMasterDataList(pageNumber) {
    var limit = $(".txt-limit").val();
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = "";
    callApi({
        url: baseUrl+ `/dms/get-sale-employee-master-data?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: true,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-sale-employee tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td><a href="${baseUrl}/dms/sale-employee-detail/${x.SlpCode}">${x.SalesCode}</a></td>
                                <td><a href="${baseUrl}/dms/sale-employee-detail/${x.SlpCode}">${x.SalesName}</a></td>
                                <td>${x.Email ?? "NON"}</td>
                                <td>${x.Telephone ?? "NON"}</td>
                                <td>${x.WhsName ?? "NON"}</td>
                                <td>
                                    <div class="badge bg-${x.Status == 'Active' ? 'success' : 'danger'}" role="alert">
                                        ${x.Status ?? "NON"}
                                    </div>
                                </td>
                            </tr>`;
            }
            $(".table-sale-employee tbody").append(tr);
            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetSaleEmlpoyeeMasterDataList);
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}
function GetReasonList(pageNumber) {
    var limit = $(".txt-limit").val()||20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = "";

    callApi({
        url:baseUrl+ `/dms/get-reason-list?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: true,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-reason tbody").empty();
            var i = 0;
            let tr = "";
            for (var x of data.data) {
                tr += `<tr>
                        <td>${i+1}</td>
                        <td class="td-reason-code">${x.Code}</td>
                        <td class="td-reason-en">${x.Reason1}</td>
                        <td class="td-reason-kh">${x.ReasonKH}</td>
                        <td class="td-createdby-name">Manager</td>
                        <td class="td-createddate">${convertDateFormat(x.CreatedDate)}</td>
                        <td class="td-reason-status">
                            <div class="badge bg-${x.Status == "Active" ? "success" : "danger"}" role="alert">
                                ${x.Status ?? "NON"}
                            </div>
                        </td>
                        <td>
                            <div class="btn p-0 btn-edit-reason" style="color:green;"><i class="fa-solid fa-pen-to-square"></i></div>
                        </td>
                    </tr>`;
                i++;
            }
            $(".table-reason tbody").append(tr);

            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetReasonList);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}
function GetPromotionList(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = "";
    const status = $(".txt-status").val() || "All";
    const fromdate = $(".txt-from-date").val().split('-')[2] + "/" + $(".txt-from-date").val().split('-')[1] + "/" + $(".txt-from-date").val().split('-')[0];
    const todate = $(".txt-to-date").val().split('-')[2] + "/" + $(".txt-to-date").val().split('-')[1] + "/" + $(".txt-to-date").val().split('-')[0];
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl+ '/dms/search-promotion-list',
        data: 
            {
                start: start,
                limit: limit,
                status: status,
                type: type,
                search: search,
                salecode: salecode,
                fromdate: fromdate,
                todate: todate,
            },
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-sale-employee tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td><a href="${baseUrl}/dms/promotion-detail/${encodeURIComponent(x.ProEntry)}">${x.Code}</a></td>
                                 <td>${x.U_ProDesc ?? ""}</td>
                                <td>${x.U_PromoType ?? ""}</td>
                                <td>${x.U_PromoFrmDate}</td>
                                <td>${x.U_PromoToDate}</td>
                                <td>
                                    <div class="badge bg-${x.U_PromoStatus == 'Active' ? 'success' : 'danger'}" role="alert">
                                        ${x.U_PromoStatus??"None"}
                                    </div>
                                </td>
                            </tr>`;
            }
            $(".table-sale-employee tbody").append(tr);
            //Set Page index
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetPromotionList);
        },
        error: function (error) {
            console.log(error);
            ShowError(error.responseText);
        }
    });
}

function getCustomerBySaleEmp_visitplan(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = $("#combobox_saleemployee").val() || "";
    
    callApi({
        url: baseUrl + `/dms/get-bp-master-data-plan?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-customer-list tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                            <td>${x.RowNumber}</td>
                            <td class="td-cardcode">${x.CardCode}</a></td>
                            <td class="td-cardname">${x.CardName}</td>
                            <td class="td-cardfname">${x.CardFName ?? "NON"}</td>
                            <td class="td-cardaddress">${x.U_AddressEn ?? "NON"}</td>
                            <td class="td-cardphone">${x.Phone1}</td>
                            <td class="td-cardcheck"><input type="checkbox" class="checkbox-customer" id="check_customer_${x.CardCode}"/></td>
                        </tr>`;
            }
            $(".table-customer-list tbody").append(tr);
            // Set pagination
            //const totalrow = data.totalrow;
            //buildPagination(totalrow, pageNumber, limit, getCustomerBySaleEmp);
            $("#txt-search-bp").on("keyup change", function () {
                var value = $(this).val().toLowerCase().replace(" ", ""); // remove all spaces
                $(".table-customer-list tbody tr").filter(function () {
                    var cardName = $(this).find(".td-cardname").text().toLowerCase().replace(" ", ""); // remove spaces too
                    $(this).toggle(cardName.includes(value));
                });
            });
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
    SetCheckboxCustomer();
}
function getCustomerBySaleEmp(pageNumber) {
    var limit = $(".txt-limit").val()||20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = "";
    callApi({
        url:baseUrl+ `/dms/get-bp-master-data?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-customer-list tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                            <td>${x.RowNumber}</td>
                            <td class="td-cardcode">${x.CardCode}</a></td>
                            <td class="td-cardname">${x.CardName}</td>
                            <td class="td-cardfname">${x.CardFName ?? "NON"}</td>
                            <td class="td-cardaddress">${x.U_AddressEn ?? "NON"}</td>
                            <td class="td-cardphone">${x.Phone1}</td>
                            <td class="td-cardcheck"><input type="checkbox" class="checkbox-customer" id="check_customer_${x.CardCode}"/></td>
                        </tr>`;
            }
            $(".table-customer-list tbody").append(tr);
            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, getCustomerBySaleEmp);
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
    SetCheckboxCustomer();
}
function SetCheckboxCustomer() {
    var dateText = $(".txt-date").text();

    // Find all customers for the selected date
    var customers = (list_customer_date || []).filter(function (obj) {
        return obj.DateText === dateText;
    });

    console.log("Customers for", dateText, customers);

    // First, clear all existing checkboxes
    $(".checkbox-customer").prop("checked", false);

    // Then, check only those in the list
    for (const x of customers) {
        var $chk = $("#check_customer_" + x.CardCode);
        if ($chk.length) {
            $chk.prop("checked", true);
        } else {
            console.warn("Checkbox not found for CardCode:", x.CardCode);
        }
    }
}
function GetPlanTrackingList(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = $("#combobox_salesman").val();
    callApi({
        url: baseUrl +  `/dms/get-visit-plan-list?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            console.log(data);
            $(".table-item tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                            <td>${x.RowNumber}</td>
                            <td><a href="${baseUrl}/dms/edit-visit-plan/${x.DocEntry}">${x.DocNum}</a></td>
                            <td>${x.U_SalesCode}</td>
                            <td>${x.SlpName}</td>
                            <td>${x.DocYear ?? "NON"}</td>
                            <td>${x.CreatedDate}</td>
                            <td>${x.Creator ?? "NON"}</td>
                            <td>${x.Remark ?? "NON"}</td>
                            <td>
                                <div class="badge bg-${x.Status == 'Active' ? 'success' : 'danger'}" role="alert">
                                    ${x.Status ?? "NON"}
                                </div>
                            </td>
                        </tr>`;
            }
            $(".table-item tbody").append(tr);
            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetPlanTrackingList);   
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}


function setDefaultMonthActive() {
    const currentMonth = new Date().getMonth() + 1;

    $(".btn-tab-month").removeClass("active");
    $(".content-month-tab").hide();

    const monthTab = $(`.btn-tab-month-${currentMonth}`);
    const monthContent = $(`.content-month-tab-${currentMonth}`);

    if (monthTab.length && monthContent.length) {
        monthTab.addClass("active");
        monthContent.show();
    } else {
        // Fallback to January if current month elements are missing
        $(".btn-tab-month-1").addClass("active");
        $(".content-month-tab-1").show();
        console.warn(`Month tab or content for month ${currentMonth} not found, falling back to January.`);
    }
}
var year;
$(document).ready(function () {
    // Cache DOM selectors
    const $yearCombobox = $(".conbobox-year"); // Fixed typo from conbobox-year
    const $monthTabs = $(".btn-tab-month");
    const $monthContents = $(".content-month-tab");

    // Initialize variables
    let year = $("#txt-docentry").val() != 0 ? $yearCombobox.val() : new Date().getFullYear().toString();
    // Set the dropdown to the current year if no value is selected
    $yearCombobox.val(year);


    // Set default month to current month
    setDefaultMonthActive();

    // Render all 12 months
    if ($(".content-month").length) {
        Render12Month(year, []);
    } else {
        //console.error("Calendar container (.content-month) not found.");
    }

    // Handle month tab clicks
    $monthTabs.click(function () {
        const datamonth = $(this).data("month");
        if (!datamonth) {
            console.warn("Month data attribute missing on clicked tab.");
            return;
        }
        $monthTabs.removeClass("active");
        $(this).addClass("active");
        $monthContents.fadeOut(200);
        const $monthContent = $(`.content-month-tab-${datamonth}`);
        if ($monthContent.length) {
            $monthContent.fadeIn(200);
        } else {
            console.warn(`Content for month ${datamonth} not found.`);
        }
    });
    // Handle year change
    $(".conbobox-year").change(function () {
        ChangeYear();
    });

    $("body").on("dblclick", ".btn-day", function () {
        const dayText = $(this)
            .clone()               // Clone the element to avoid modifying the original
            .find("span").remove() // Remove all <span> elements from the clone
            .end()                 // Back to the cloned div
            .text().trim();        // Extract the day number

        const monthText = $(this).data("month"); // ✅ from data-month
        const yearText = $(this).data("year");   // ✅ from data-year

        LoadCustomerPopup_VisitPlan(dayText, monthText, yearText, 0);
    });

    //$("body").on("click", ".btn-day", function () {
    //    const dayText = $(this)
    //        .clone()               // Clone the element to avoid modifying the original
    //        .find("span").remove() // Remove all <span> elements from the clone
    //        .end()                 // Back to the cloned div
    //        .text().trim();        // Extract the day number

    //    const monthText = $(this).data("month"); // ✅ from data-month
    //    const yearText = $(this).data("year");   // ✅ from data-year

    //    //LoadCustomerPopup_VisitPlan(dayText, monthText, yearText, 0);
    //    alert("We are implement for recuring and move next day");
    //});

    // small helper to avoid XSS when inserting text into HTML
    function escapeHtml(str) {
        if (str == null) return "";
        return String(str)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#039;');
    }

    // show popup on hover; use mouseenter to avoid child-bubbling issues
    $("body").on("mouseenter", ".btn-day", function () {
        const $btn = $(this);

        // get day text without badge <span>
        const dayText = $btn.clone().find("span").remove().end().text().trim();
        // month and year from the parent .month element (data attributes)
        const monthAttr = $btn.closest(".month").data("month");
        let yearAttr = $btn.closest(".month").data("year");

        // fallback if data-year isn't present
        if (!yearAttr) yearAttr = parseInt($(".conbobox-year").val(), 10) || new Date().getFullYear();

        const day = parseInt(dayText, 10);
        const month = parseInt(monthAttr, 10);
        const year = parseInt(yearAttr, 10);

        if (isNaN(day) || isNaN(month) || isNaN(year)) {
            // invalid date — hide popup and exit
            $(".pop-hover-list").hide();
            return;
        }

        // normalize DateText to dd-MM-yyyy (matches list_customer_date DateText)
        const DateText = `${String(day).padStart(2, '0')}-${String(month).padStart(2, '0')}-${String(year)}`;

        // safe filter (list_customer_date might be undefined)
        const customers = (list_customer_date || []).filter(obj => obj.DateText === DateText);

        // build rows safely
        let rowHtml = '';
        let i = 1;
        for (const x of customers) {
            rowHtml += `<tr>
                        <td>${i}</td>
                        <td>${escapeHtml(x.CardCode)}</td>
                        <td>${escapeHtml(x.CardName)}</td>
                    </tr>`;
            i++;
        }
        if (rowHtml === '') {
            rowHtml = '<tr><td colspan="3" class="text-center">No customers</td></tr>';
        }

        // insert rows
        const $tbody = $(".table-pop-list tbody");
        $tbody.html(rowHtml);

        // position popup: show it invisibly to measure height, then position and reveal
        const $popup = $(".pop-hover-list");
        $popup.css({ display: 'block', visibility: 'hidden' }); // render but invisible
        const rect = this.getBoundingClientRect();
        const popupHeight = $popup.outerHeight() || 100;
        const bottomSpace = $(window).height() - rect.bottom;
        const topSpace = rect.top;

        let top, left;
        if (bottomSpace < popupHeight && topSpace > popupHeight) {
            // show above
            top = rect.top + $(window).scrollTop() - popupHeight - 10;
        } else {
            // show below
            top = rect.bottom + $(window).scrollTop() + 10;
        }
        left = rect.left + $(window).scrollLeft();

        $popup.css({
            top: top + 'px',
            left: left + 'px',
            visibility: 'visible'
        });
    });

    // hide when leaving the day — allow small delay so moving into popup keeps it open
    $("body").on("mouseleave", ".btn-day", function () {
        const $popup = $(".pop-hover-list");
        // small delay so user can move cursor into popup
        setTimeout(function () {
            if (!$popup.is(':hover')) {
                $popup.hide();
            }
        }, 100);
    });

    // keep popup visible when hovering it, hide when leaving popup
    $(".pop-hover-list")
        .on("mouseenter", function () {
            $(this).show();
        })
        .on("mouseleave", function () {
            $(this).hide();
        });

    function LoadCustomerPopup_VisitPlan(day, month, year, key) {
        showLoading();
        // Remove any existing modal first
        $("#customerModal").remove();

        // Create modal HTML dynamically
        var popup = `
            <div class="modal fade" id="customerModal" tabindex="-1" aria-hidden="true">
              <div class="modal-dialog modal-lg" style="min-width:75%">
                <div class="modal-content">
                  <div class="modal-header">
                        <h5 class="modal-title" id="customerModal">Customer Information</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                  <div class="modal-body" id="customerModalBody">
                    <div class="text-center p-3">Loading...</div>
                  </div>
                </div>
              </div>
            </div>
            `;

        $("body").append(popup);

        // Load content into modal body
        $("#customerModalBody").load(
            baseUrl + "/dms/popup-customer?day=" + day + "&month=" + month + "&year=" + year + "&key=" + key,
            function (responseTxt, statusTxt, xhr) {
                hideLoading();
                if (statusTxt === "success") {
                    // Show the modal using Bootstrap JS API
                    var modal = new bootstrap.Modal(document.getElementById('customerModal'));
                    modal.show();
                } else {
                    ShowError("Error when loading Customer Popup");
                }
            }
        );
    }

    function LoadCustomerPopup(day, month, year, key) {
        showLoading();

        // Remove any existing modal first
        $("#customerModal").remove();

        // Create modal HTML dynamically
        var popup = `
            <div class="modal fade" id="customerModal" tabindex="-1" aria-hidden="true">
              <div class="modal-dialog modal-lg" style="min-width:75%">
                <div class="modal-content">
                  <div class="modal-header">
                        <h5 class="modal-title" id="customerModal">Customer Information</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                  <div class="modal-body" id="customerModalBody">
                    <div class="text-center p-3">Loading...</div>
                  </div>
                </div>
              </div>
            </div>
            `;

        $("body").append(popup);

        // Load content into modal body
        $("#customerModalBody").load(
            baseUrl + "/dms/popup-customer?day=" + day + "&month=" + month + "&year=" + year + "&key=" + key,
            function (responseTxt, statusTxt, xhr) {
                hideLoading();
                if (statusTxt === "success") {
                    // Show the modal using Bootstrap JS API
                    var modal = new bootstrap.Modal(document.getElementById('customerModal'));
                    modal.show();
                } else {
                    ShowError("Error when loading Customer Popup");
                }
            }
        );
    }
    function CloseCustomerPopup() {
        /*$(".pop-customer-overlay").remove();*/
        $("#customerModal").modal("hide");
    }
    $("body").on("click", ".btn-close-pop-customer", function () {
        CloseCustomerPopup();
        ClosePopupSOBPList();
        ClosePopupSOItemList();
    });
    $("body").on("click", ".btn-cancel-customer", function () {
        CloseCustomerPopup();
        ClosePopupSOBPList();
        ClosePopupSOItemList();
    });
    $("body").on("click", ".pop-customer-overlay", function (e) {
        const bg_overlay = $(".pop-customer-overlay")[0];
        if (e.target === bg_overlay) {
            CloseCustomerPopup();
        }
    });
    $("body").on("click", ".pop-so-bp-list-overlay", function (e) {
        const bg_overlay = $(".pop-so-bp-list-overlay")[0];
        if (e.target === bg_overlay) {
            ClosePopupSOBPList();
        }
    });
    $("body").on("click", ".btn-add-customer", function () {
        var datetext = $(".txt-date").text().trim(); // could be "dd-MM-yyyy" or "yyyy-MM-dd"
        console.log(datetext)
        var day, month, year;

        var dates = datetext.split("-");
        if (dates.length === 3) {
            // If year first (yyyy-MM-dd)
            if (dates[0].length === 4) {
                year = parseInt(dates[0], 10);
                month = parseInt(dates[1], 10);
                day = parseInt(dates[2], 10);
            }
            // If day first (dd-MM-yyyy)
            else {
                day = parseInt(dates[0], 10);
                month = parseInt(dates[1], 10);
                year = parseInt(dates[2], 10);
            }
        }

        // Remove all existing entries for this date
        list_customer_date = list_customer_date.filter(function (data) {
            return data.DateText !== datetext;
        });

        // Reset the badge for this day
        setDayBadge(year, day, month, 0);

        var count = 0;

        $(".table-customer-list tbody tr").each(function () {
            var row = $(this);
            var checkbox = row.find(".td-cardcheck .checkbox-customer");
            if (checkbox.is(':checked')) {
                var obj = {
                    DateText: datetext,
                    VisitDate: `${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`,
                    CardCode: row.find(".td-cardcode").text(),
                    CardName: row.find(".td-cardname").text(),
                };

                // Remove duplicate if exists
                var existingIndex = list_customer_date.findIndex(function (data) {
                    return data.DateText === obj.DateText && data.CardCode === obj.CardCode;
                });
                if (existingIndex !== -1) {
                    list_customer_date.splice(existingIndex, 1);
                }

                // Add the new customer
                list_customer_date.push(obj);
                count++;
            }
        });

        // Set the badge with correct count and year
        if (count > 0) {
            setDayBadge(year, day, month, count);
        }

        CloseCustomerPopup();
    });


    $("body").on("click", "#btn-save-plan", function () {
        saveVisitPlan();
    });
    $("body").on("click", "#btn-save-so", function () {
        SaveSO();
    });

    $("body").on("click", "#btn-manager-update", function () {
        ManagerUpdateSOGeneralBP();
    });

    $("body").on("click", "#btn-update-so", function () {
        UpdateSOGeneralBP();
    });

    $("body").on("click", "#btn-so_run_promotion", function () {
        //SaveSO();
        RunPromotion();
        promotionRun = true;
    });
    $("body").on("change", ".combobox-taxtype", function () {
        //SaveSO();
        //RunPromotion();
        CalculateDocTotal();
    });

    $("body").on("click", "#btn-integrate-so", function () {
        ReIntegrateSO();
    });
    $("body").on("click", "#btn-approved", function () {
        ApproveReject("Approved");
    });
    $("body").on("click", "#btn-reject", function () {
        ApproveReject("Rejected");
    });
    $("body").on("click", "#btn-cancel", function () {
        ApproveReject("Cancelled");
    });
    //$("body").on("click", "#btn-save-so", function () {
    //    SaveSO();
    //});

    $("body").on("click", ".btn-download-template", function () {
        DownloadFileTemplate();
    });
    $("body").on("change", ".file-import", function (e) {
        list_customer_date = [];
        notifications = [];
        var formData = new FormData();
        formData.append("importFile", $(".file-import")[0].files[0]);
        callApi({
            url: baseUrl + '/DMS/ReadExcel',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            async: false,
            beforeSend: function () {
                showLoading()
            },
            complete: function () {
                hideLoading();
            },
            success: function (data) {
                if (data.success === true) {

                    var rows = data.data;
                    //Set Header
                    $(".conbobox-sale-emp").val(rows[0].SaleCode);
                    $(".conbobox-year").val(rows[0].DocYear);
                    ChangeYear();
                    //Set Detail
                    var dates;
                    var count = 0;
                    var oldDate = null;
                    for (var x of rows) {
                        dates = x.VisitDate.split("-");
                        var obj = {
                            DateText: x.VisitDate,
                            VisitDate: dates[2] + "-" + dates[1] + "-" + dates[0],
                            CardCode: x.CardCode,
                            CardName: x.CardName,
                        }
                        list_customer_date.push(obj);

                        if (oldDate == null) {
                            oldDate = x.VisitDate;
                            count = 1;
                        } else if (oldDate == x.VisitDate) {
                            count = count + 1
                        } else {
                            oldDate = x.VisitDate;
                            count = 1;
                        }
                        if (count > 0) {
                            setDayBadge(parseInt(dates[0]), parseInt(dates[1]), count);
                        }
                    }
                } else {
                    ShowError( data.message);
                }
            },
            error: function (error) {
                ShowError(error.responseText);
            }
        });
    });
    $("body").on("dblclick", ".txt-cardcode", function () {
        ShowPopupSOBPList();
    });
    $("body").on("change", ".combobox-sale-emp", function () {
        $(".txt-doc-num").val(GetSObySalesman());
    });
    $("body").on("change", ".txt-doc-date", function () {
        $(".txt-doc-num").val(GetSObySalesman());
    });
    $("body").on("change", "#combobox_saleemployee", function () {
        var selectedOption = $(this).find(":selected"); 
        var code = selectedOption.data("code");
        $(".txt-docnum").val(code + "-" + $("#combobox_year").val());
    });
    $("body").on("change", "#combobox_year", function () {
        var selectedOption = $("#combobox_saleemployee").find(":selected");
        var code = selectedOption.data("code");
        $(".txt-docnum").val(code + "-" + $("#combobox_year").val());
    });

    $("body").on("click", ".table-so-customer tbody tr", function () {
        $(".table-so-customer tr").removeClass("active");
        $(this).addClass("active");
    });
    $("body").on("dblclick", ".table-so-customer tbody tr", function () {
        ChooseCustomer();
    });
    $("body").on("click", "#btn-choose-customer", function () {
        ChooseCustomer();
    });
    $("body").on("dblclick", ".btn-show-item", function () {
        ShowPopupSOItemList(1);
    });
    $("body").on("click", ".pop-so-item-list-overlay", function (e) {
        const bg_overlay = $(".pop-so-item-list-overlay")[0];
        if (e.target === bg_overlay) {
            ClosePopupSOItemList();
        }
    });
    $("body").on("click", "#btn-choose-item", function () {
        ChooseItem();
    });
    // Remove a row on double-click of the first cell
    $("body").on("dblclick", ".table-so-container tbody tr td:first-child", function () {
        const row = $(this).closest("tr");
        const rowIndex = row.index(); // Get zero-based index directly

        RemoveTRSO(rowIndex);
    });
    $("body").on("change", ".table-so-item .checkbox-item", function () {
        const checkbox = $(this);
        const row = checkbox.closest("tr");
        if (!row.length) return;

        const principle = row.find(".td-principlecode").text().trim();

        // STEP 1: Get principle from confirmed items
        var listPrinciple = item_list.length > 0 ? item_list[0].PrincipleCode : null;

        // STEP 2: Get principle from already checked rows (except this one)
        var checkedRow = $(".checkbox-item:checked").not(checkbox).first().closest("tr");
        var existingPrinciple = checkedRow.find(".td-principlecode").text().trim() || null;

        // STEP 3: Determine active principle
        var activePrinciple = listPrinciple || existingPrinciple || null;

        if (checkbox.prop("checked")) {
            // Trying to check this checkbox
            if (!activePrinciple || activePrinciple === principle) {
                row.addClass("active-row");
            } else {
                ShowWarning("You can only select items from the same principle.");
                checkbox.prop("checked", false); // uncheck if principle differs
            }
        } else {
            // Unchecking
            row.removeClass("active-row");
        }
    });

    // Row click handler (for clicks outside checkbox)
    $("body").on("click", ".table-so-item tbody tr", function (e) {
        if ($(e.target).is(".checkbox-item")) return; // already handled

        const checkbox = $(this).find(".checkbox-item");
        checkbox.prop("checked", !checkbox.prop("checked")).trigger("change");
    });


    $("body").on("click", "#btn-cancel-so", function () {
        CancelSO();
    });
    $("body").on("change", ".txt-province", function (e) {
        GetDistrict($(this).val());
    });
    $("body").on("change", ".txt-district", function (e) {
        GetCommune($(this).val());
    });
    $("body").on("change", ".txt-commune", function (e) {
        GetSelectedCommune($(this).val());
    });
    $("body").on("click", "#btn-save-bp", function (e) {
        SaveCustomerRequest();
    });
    $("body").on("click", "#btn-approve-bp", function (e) {
        ActionBP("Approved");
    });
    $("body").on("click", "#btn-reject-bp", function (e) {
        ActionBP("Rejected");
    });
    $("body").on("click", ".btn-emp-tab", function () {
        $(".btn-emp-tab").removeClass("active");
        $(this).addClass("active");

        if ($(this).data("btn-tab") === "customer") {
            $(".box-tab-customer").show();
            $(".box-tab-item").hide();
        }
        else {
            $(".box-tab-customer").hide();
            $(".box-tab-item").show();
        }
    });
    $("body").on("click", ".btn-edit-reason", function () {
        var tr = $(this).closest("tr");

        $(".txt-reason-code").val(tr.find(".td-reason-code").text().trim());
        $(".txt-reason-en").val(tr.find(".td-reason-en").text().trim());
        $(".txt-reason-kh").val(tr.find(".td-reason-kh").text().trim());
        $(".txt-reason-status").val(tr.find(".td-reason-status").children().text().trim());

        // jQuery way to show Bootstrap modal
        $("#reasonModal").modal("show");
    });
    $("body").on("click", "#btn-save-reason", function () {
        console.log(this)
        SaveReason();
    });
    $("body").on("click", ".btn-add-reason", function () {

        $(".group-code").hide();
        $(".txt-reason-code").val(0);
        $(".txt-reason-en").val("");
        $(".txt-reason-kh").val("");
        $(".txt-reason-status").val("Active");
        $("#reasonModal").modal("show");
    });
    $("body").on("change", ".btn-user-file", function () {
        const file = this.files[0];
        const previewContainer = $(".box-btn-choose");

        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                previewContainer.css({ "background-image": `url(${e.target.result})` });
            };
            reader.readAsDataURL(file);
            formdata.set("file", file);
        }
    });
    $("body").on("click", ".btn-add-user", function () {
        $("#userModal").modal("show");
        $("#userModal .modal-title").text("Add new user")
        $(".txt-company-name").val("");
        $(".txt-user-code").val("");
        $(".txt-user-code").prop('readonly', false);
        $(".txt-user-name").val("");
        $(".txt-user-email").val("");
        $(".txt-user-type").val("Admin");
        $(".txt-user-status").val("Active");
        $(".box-btn-choose").css({ "background-image": `url(${NoProfile})` });
        $(".txt-user-password").val("");
        //set hide show
        $(".block-checkbox-password").hide();
        $(".block-user-code").hide();
        $(".password-group").show();
        $(".password-group").css({ display: "flex" });
        $('.checkbox-change-password').prop('checked', false);
    });
    $("body").on("click", ".btn-edit-user", function () {
        $("#userModal").modal("show");
        $("#userModal .modal-title").text("Update new user");
        var tr = $(this).parents("tr");
        var img = tr.find(".img-user").attr("src");
        console.log(img);
        $("#userModal .modal-title").text("Update new user")
        var tr = $(this).parents("tr");
        var img = tr.find(".img-user").attr("src");
        $(".txt-company-name").val(tr.find(".td-user-company-name").text());
        $(".txt-user-code").val(tr.find(".td-user-code").text());
        $(".txt-user-name").val(tr.find(".td-user-name").text());

        $(".txt-printer-name").val(tr.find(".td-printer-name").text());
        $(".txt-printer-mac").val(tr.find(".td-printer-mac").text());

        $(".txt-user-code").prop('readonly', true);

        $(".txt-user-type").val(tr.find(".td-user-type").text());

        $(".txt-user-status").val(tr.find(".td-user-status div").text().trim());
        $(".box-btn-choose").css({
            "background-image": `url(${img == "" ? NoProfile : img})`
        });
        $(".txt-user-password").val("");
        $(".block-checkbox-password").css({ display: "flex" });
        $(".block-user-code").css({ display: "flex" });
        $(".password-group").hide();
        $('.checkbox-change-password').prop('checked', false);


        $(".txt-device-id").val(tr.find(".td-device-id").text());
        $(".txt-sales-employee").val(tr.find(".td-link-sap").text());
        $(".txt-manager").val(tr.find(".td-manager").text());
        $(".txt-closed-end-of-day").val(tr.find(".td-close-date").text());

        IsCheck = false;

    });
    $("body").on("click", ".checkbox-change-password", function () {
        IsCheck = !IsCheck;
        if (IsCheck == true) {
            $(".password-group").css({ display: "flex" });
        }
        else {
            $(".password-group").css({ display: "none" });
        }
    });
    $("body").on("click", "#btn-save-user", function () {
        SaveUser();
    });
    $("body").on("click", ".btn-profile", function () {
        ShowConfirmedAlert("Are you want to sign out off account?", function () {
            localStorage.removeItem("UserData");
            GoLoginPage();
        })
    });
});
var IsCheck = false;
var list_customer_date = [];
// Example notification data: { monthIndex: 0 (Jan), day: 5, count: 3 }
var notifications = [
    //{ monthIndex: 0, day: 5, count: 3 },
    //{ monthIndex: 0, day: 15, count: 1 },
    //{ monthIndex: 1, day: 10, count: 2 },
    //{ monthIndex: 2, day: 20, count: 4 },
    //{ monthIndex: 3, day: 8, count: 4 },
    //{ monthIndex: 4, day: 7, count: 21 },
];
function hideLoading() {
    $(".loading").hide();
}
function showLoading() {
    $(".loading").show();
}

// Render a single month
function renderMonth(year, index, target, notifications = []) {
    // Leap year check
    const isLeapYear = (year % 4 === 0 && year % 100 !== 0) || (year % 400 === 0);

    const months = [
        { name: "January", days: 31 },
        { name: "February", days: isLeapYear ? 29 : 28 },
        { name: "March", days: 31 },
        { name: "April", days: 30 },
        { name: "May", days: 31 },
        { name: "June", days: 30 },
        { name: "July", days: 31 },
        { name: "August", days: 31 },
        { name: "September", days: 30 },
        { name: "October", days: 31 },
        { name: "November", days: 30 },
        { name: "December", days: 31 },
    ];

    const weekDays = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
    const month = months[index];

    const firstDay = new Date(year, index, 1).getDay(); // Sunday=0
    const daysInMonth = month.days;

    let html = `
            <div class="month" data-month="${index + 1}" data-year="${year}">
                <h3>${month.name} ${year}</h3>
                <div class="days">
                    ${weekDays.map(day => `<div class="day header">${day}</div>`).join("")}

                    
                    `;

    // Empty slots for alignment
    for (let i = 0; i < (firstDay === 0 ? 6 : firstDay - 1); i++) {
        html += `<div class="day empty"></div>`;
    }

    // Add each day
    for (let day = 1; day <= daysInMonth; day++) {
        const notification = notifications.find(
            n => n.monthIndex === index && n.day === day && n.year === year
        );

        const notificationBadge = notification
            ? `<span class="notification-badge">${notification.count}</span>`
            : "";

        html += `
                <div class="day btn-day" 
                     data-day="${day}" 
                     data-month="${index + 1}" 
                     data-year="${year}">
                    ${day} ${notificationBadge}
                </div>`;
    }
    if (index < 11) {
        html += `</div><button class="btn btn-primary" data-curmonth="${index + 1}">&nbsp;Move Next Month</button></div>`;
    }
    else {
        html += `</div></div>`;
    }
    $(target).html(html);
}

// Render all 12 months for a specific year
function Render12Month(year, notifications = []) {
    $(".content-month").html('<div class="loading">Loading calendar...</div>');
    for (let i = 0; i < 12; i++) {
        renderMonth(year, i, `.content-month-${i + 1}`, notifications);
    }
}

// Set a badge for a specific day/month/year
function setDayBadge(year, day, month, count) {
    // month = 1-based
    var obj = { year: year, monthIndex: month - 1, day: day, count: count };

    // Remove existing badge for this date/year
    var existingIndex = notifications.findIndex(n =>
        n.year === obj.year &&
        n.monthIndex === obj.monthIndex &&
        n.day === obj.day
    );

    if (existingIndex !== -1) {
        notifications.splice(existingIndex, 1);
    }

    if (count > 0) {
        notifications.push(obj);
    }

    // Re-render calendar for the correct year
    Render12Month(year, notifications);
}
function setActiveMonth(month) {
    $(".btn-tab-month").removeClass("active");
    $(".content-month-tab").hide();
    $(`.btn-tab-month-${month}`).addClass("active");
    $(`.content-month-tab-${month}`).show();
}
function initializeMonthTabs(currentMonth) {
    // Set default active month
    $(".btn-tab-month").removeClass("active");
    $(".content-month-tab").hide();
    $(`.btn-tab-month-${currentMonth}`).addClass("active");
    $(`.content-month-tab-${currentMonth}`).show();
}
// Example: populate badges from your VisitPlanDto
function loadVisitPlan(visitPlanDto) {
    const header = visitPlanDto.header;
    const detail = visitPlanDto.detail;

    const docYear = header.DocYear;

    notifications = []; // clear previous badges

    // Count visits per day
    const counts = {};
    detail.forEach(d => {
        const date = new Date(d.VisitDate);
        const key = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
        counts[key] = (counts[key] || 0) + 1;
    });

    // Set badges
    Object.keys(counts).forEach(key => {
        const [y, m, d] = key.split("-").map(Number);
        setDayBadge(y, d, m, counts[key]);
    });

    // Render calendar for docYear
    Render12Month(docYear, notifications);
}

function saveVisitPlan() {
    if ($("#combobox_saleemployee").val() == "") {
        ShowWarning("Sale Employee cannot be blank!");
        return;
    }
    if (list_customer_date.length === 0) {
        ShowWarning("Customer cannot be blank!");
        return;
    }

    // Prepare header
    var header = {
        docEntry: parseInt($("#txt-docentry").val()) || 0,
        salesCode: parseInt($(".conbobox-sale-emp").val()) || 0,
        docYear: parseInt($(".conbobox-year").val()) || 0,
        remark: $("#txt-remark").val() || "",
        createdBy: UserData?.Code || "SYSTEM",
    };

    // Prepare details
    var details = list_customer_date.map(c => ({
        docEntry: header.docEntry,
        visitDate: formatDate(c.VisitDate), // <- padded
        cardCode: c.CardCode,
        reasonType: c.ReasonType || "",
        remark: c.Remark || "",
        imageURL: c.ImageURL || ""
    }));

    // Combine into VisitPlanDto
    var visitPlanDto = {
        header: header,
        detail: details
    };

    console.log("Saving Visit Plan:", JSON.stringify(visitPlanDto));

    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/save-visit-plan',
        data: JSON.stringify(visitPlanDto),
        async: true,
        beforeSend: showLoading,
        complete: hideLoading,
        success: function (data) {
            if (data.code === 200) {
                ShowReloadAlert("Save Success.");
            } else {
                ShowError(data.message);
            }
        },
        error: function (error) {
            ShowError( error.responseText || "Error saving Visit Plan.");
        }
    });
}


function OpenVisitPlan(key) {
    window.location.href = `${baseUrl}/DMS/EditVisitPlan?key=` + key;
}
function GetVisitPlanDetail(key) {
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-visit-plan-detail/' + key,
        async: false,
        beforeSend: showLoading,
        complete: hideLoading,
        success: function (data) {
            console.log(data);

            list_customer_date = []; // clear previous data
            notifications = [];      // clear previous badges

            const header = data.header;
            const detail = data.detail;

            // Set header values safely
            $(".txt-docnum").val(header?.DocNum || "");
            $(".conbobox-sale-emp").val(header?.SalesCode || "");
            $(".conbobox-year").val(header?.DocYear || new Date().getFullYear());

            if (detail && detail.length > 0) {
                const counts = {}; // to count visits per day

                for (const x of detail) {
                    if (!x || !x.DateText) continue;

                    const parts = x.DateText.split("-"); // ["dd","MM","yyyy"]
                    if (!parts || parts.length !== 3) continue;

                    const day = parseInt(parts[0], 10);
                    const month = parseInt(parts[1], 10);
                    const year = parseInt(parts[2], 10);

                    // Add to customer list
                    const visitDate = `${year}-${String(month).padStart(2, '0')}-${String(day).padStart(2, '0')}`;
                    list_customer_date.push({
                        DateText: x.DateText,
                        VisitDate: visitDate,
                        CardCode: x.CardCode || "",
                        CardName: x.CardName || ""
                    });

                    // Count visits per day
                    counts[visitDate] = (counts[visitDate] || 0) + 1;
                }

                // Set badges using counts
                for (const k in counts) {
                    if (!counts.hasOwnProperty(k)) continue;

                    const parts = k.split("-");
                    const y = parseInt(parts[0], 10);
                    const m = parseInt(parts[1], 10);
                    const d = parseInt(parts[2], 10);

                    setDayBadge(y, d, m, counts[k]);
                }
            }

            // Render calendar for the plan's year
            Render12Month(header?.DocYear || new Date().getFullYear(), notifications);
        },
        error: function (error) {
            ShowError(error.responseText || "Error loading visit plan.");
            console.log(error);
        }
    });
}

function DownloadFileTemplate() {
    window.open("/DMS/DownloadTemplate");
}
function ChangeYear() {
    // Get selected year from dropdown
    var selectedYear = parseInt($(".conbobox-year").val());
    year = selectedYear;
    // Clear the calendar container
    $(".content-month").empty();

    // Re-render all 12 months for the selected year
    Render12Month(selectedYear, notifications);

    // Optionally set default active month
    setDefaultMonthActive();
}
function getSOCustomerList(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-so-search-bp").val() || "";
    const salecode = "";
    callApi({
        url: baseUrl +  `/dms/get-bp-master-data?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}`,
        type: 'GET',
        dataType: 'json',
        async: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-so-customer tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td class="td-cardcode">${x.CardCode}</td>
                                <td class="td-cardname">${x.CardName}</td>
                                <td class="td-cardfname">${x.CardFName ?? "NON"}</td>
                                <td class="td-cardaddress">${x.FullAddEN ?? ""}</td>
                                <td class="td-cardphone">${x.Phone1 ?? ""}</td>
                                <td class="td-cardtaxid">${x.LicTradNum ?? ""}</td>
                                <td class="td-pricelist" style='display:none'>${x.ListNum ?? ""}</td>
                            </tr>`;
            }
            $(".table-so-customer tbody").append(tr);
            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, getSOCustomerList);
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}
function ClosePopupSOBPList() {
    $(".pop-so-bp-list-overlay").hide(200);
}
function ShowPopupSOBPList() {
    getSOCustomerList(1);
    //$(".pop-so-bp-list-overlay").show(200);
    $("#customerListModal").modal('show');
}
function ChooseCustomer() {
    var cardcode = "";
    var cardname = "";
    var contactper = "";   
    var taxid = "";
    var address = "";
    cardcode = $(".table-so-customer tr.active").find(".td-cardcode").text() ?? "";
    cardname = $(".table-so-customer tr.active").find(".td-cardname").text() ?? "";
    contactper = $(".table-so-customer tr.active").find(".td-cardphone").text() ?? "";
    taxid = $(".table-so-customer tr.active").find(".td-cardtaxid").text() ?? "";
    address = $(".table-so-customer tr.active").find(".td-cardaddress").text() ?? "";

    var pricelist = $(".table-so-customer tr.active").find(".td-pricelist").text() ?? "";
    $(".txt-cardcode").val(cardcode);
    $(".txt-cardname").val(cardname);
    $(".txt-contactper").val(contactper);
    $(".txt-taxid").val(taxid);
    $(".def_pricelist").val(pricelist);
    $(".txt-address").val(address);

    //ClosePopupSOBPList();
    $("#customerListModal").modal('hide');
    CalculateDocTotal();
    GetContactPerson(cardcode);
    GetSalesmanbyCardCode(cardcode);
}
function ShowPopupSOItemList() {
    getSOItemList(1);
    //$(".pop-so-item-list-overlay").show(200);
    $("#itemListModal").modal("show");
    SetCheckboxItem();
}
function ClosePopupSOItemList() {
    $(".pop-so-item-list-overlay").hide(200);
}
function getSOItemList(pageNumber) {
    var limit = $(".txt-limit").val()||20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-so-search-item").val() || "";
    const salecode = "";
    const principle = $(".txt-principle").val() || "All";
    callApi({
        url: baseUrl +  `/dms/get-item-master-data?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}&principle=${principle}`,
        type: 'GET',
        dataType: 'json',
        async: false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-so-item tbody").empty();
            var tr = "";
            for (var x of data.data) {
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td class="td-itemcode">${x.ItemCode}</td>
                                <td class="td-itemname">${x.ItemName}</td>
                                <td class="td-itemgroup">${x.ItemGroupName ?? "NON"}</td>
                                <td class="td-promotion">${x.HasPromotion ?? "NON"}</td>
                                <td>${x.PrincipleName ?? "NON"}</td>
                                <td class="td-onhand" style='display:none'>${x.Onhand ?? ""}</td>
                                <td class="td-defuomentry" style='display:none'>${x.DefEntry ?? -1}</td>
                                <td class="td-check"><input type="checkbox" class="checkbox-item" id="check_item_${x.ItemCode}"/></td>
                                <td class="td-principlecode" style='display:none'>${x.PrincipleCode ?? ""}</td>
                                <td class="td-maincatcode" style='display:none'>${x.MainCatCode ?? ""}</td>
                                <td class="td-subcatcode" style='display:none'>${x.SubCatCode ?? ""}</td>
                            </tr>`;
            }
            $(".table-so-item tbody").append(tr);
            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, getSOItemList);
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}

//function Verify_Checked_Item_Priciple() {
//    $(document).on('change', '.checkbox-item', function () {
//        const row = $(this).closest('tr');
//        const principlecode = row.find('.td-principlecode').text();
//        ShowWarnning(principlecode);
//    });
//}

var item_list = [];
function ChooseItem() {
    const selectedItems = $(".table-so-item tbody tr").filter(function () {
        return $(this).find(".td-check .checkbox-item").is(":checked");
    });

    selectedItems.each(function () {
        const row = $(this);
        const obj = {
            LineNum: -1,
            ItemCode: row.find(".td-itemcode").text(),
            ItemName: row.find(".td-itemname").text(),
            defEntry: row.find(".td-defuomentry").text(),
            PrincipleCode: row.find(".td-principlecode").text(),
            MainCatCode: row.find(".td-maincatcode").text(),
            SubCatCode: row.find(".td-subcatcode").text(),
            Quantity: 1,
            UnitPrice: 0,
            DisPer: 0,
            DisAmount: 0,
            LineTotal: 0,
            UomEntry: -2,
            WhsCode: "WHO1",
            SaleType: "SAL",
            ProEntry: -1,
            ProLine: -1,
            ProType: "",
            UoMName: "",
            IsPromotion: "No",
            WhsName: "",
            InStock: 0,
            PromotionType: "",
            LineStatus: "Active",
            // 🔥 CRITICAL FLAG
            IsNew: true   // 👈 This tells system: "Get new price from service"
        };

        item_list.push(obj);
    });

    $("#itemListModal").modal("hide");
    GetWhsList();
    renderItemTable();
    CalculateDocTotal();
}

function returnstringvalue(val) {
    var x = "";
    try {
        x = val.replace(",", "");
        x = x.replace(",", "");
        x = x.replace(",", "");
        x = x.replace(",", "");
        x = x.replace(",", "");
    }
    catch (ex) { x = val; }
    return x;
}
function SetCheckboxItem() {
    for (var x of item_list) {
        //$("#check_item_" + x.ItemCode).prop('checked', true);
    }
}
// Render table from item_list
function renderItemTable() {
    const tbody = $(".table-so-container tbody").empty();
    const whsList = JSON.parse(localStorage.getItem("whslist") || "[]");

    item_list.forEach((item, index) => {

        const uomOptions = GetItemUOM(
            item.ItemCode,
            item.UomEntry == -2 ? item.defEntry : item.UomEntry
        );

        const whsOptions = whsList.map(w =>
            `<option value="${w.WhsCode}" ${w.WhsCode === item.WhsCode ? "selected" : ""}>
                ${w.WhsName}
            </option>`).join('');

        const row = $(`
            <tr data-line-num="${item.LineNum}" data-line-status="${item.LineStatus}">
                <td><input type="text" class="td-input" value="${index + 1}" readonly></td>
                <td><input type="text" class="td-input txt-item-code" value="${item.ItemCode}" readonly disabled></td>
                <td><input type="text" class="td-input txt-item-name" value="${item.ItemName}"></td>
                <td>${uomOptions}</td>
                <td><select class="td-input txt-whs-code">${whsOptions}</select></td>
                <td><input type="text" class="td-input td-input-right txt-line-instock" readonly disabled></td>
                <td><input type="text" class="td-input td-input-right txt-line-qty" value="${item.Quantity}"></td>
                <td><input type="text" class="td-input td-input-right txt-line-price" value="${To2Digit(item.UnitPrice)}"></td>
                <td><input type="text" class="td-input td-input-right txt-line-discount" readonly disabled value="${To2Digit(item.DisPer)}"></td>
                <td><input type="text" class="td-input td-input-right txt-line-discount-amt" readonly disabled value="${To2Digit(item.DisAmount)}"></td>
                <td><input type="text" class="td-input td-input-right txt-line-total" readonly disabled value="${To2Digit(item.LineTotal)}"></td>
            </tr>
        `);

        tbody.append(row);
        let lineuom = row.find(".txt-uom-code").val();
        let linewhs = row.find(".txt-whs-code").val();
        // Always refresh stock
        const stock = GetItemStock(item.ItemCode, lineuom, linewhs);
        row.find(".txt-line-instock").val(To2Digit(stock));
/*        row.find(".txt-uom-code").trigger("change");*/

        // Price logic:
        // 1) ADD mode -> always get new price
        // 2) OPEN mode -> only get new price for newly added rows
        if (item.IsNew === true) {
            
            const price = GetItemPrice(item.ItemCode,lineuom , 1, item.WhsCode);

            row.find(".txt-line-price").val(To2Digit(price));

            item.UnitPrice = price;
            item.LineTotal = item.Quantity * price;

            row.find(".txt-line-total").val(To2Digit(item.LineTotal));

            // Reset after first pricing
            item.IsNew = false;
        }

    });

    bindRowCalculation();
    formatInputToTwoDigits();

    $(".table-so-container tfoot tr:eq(0) td:eq(0) input")
        .val($(".table-so-container tbody tr").length + 1);
}

function GetItemWarehouse() {
    var getStock = GetItemStock(data.ItemCode, data.DefUoM, $(this).find('.txt-whs-code').val());
    $(this).find(".txt-line-instock").val(To2Digit(getStock));
}
function GetItemUOM(itemcode) {
        //if (data.UoMEntry != null) {
        //    $(this).find(".txt-uom-code").val(data.DefUoM);
        //}
    //alert("OK");
        $(this).find(".txt-uom-code").val(data.DefUoM);

        //if (data.WhsCode != null) {
        //    $(this).find(".txt-whs-code").val(data.WhsCode);
    //}
   // alert("GetItemUOM");
        $(this).find(".txt-whs-code").val(data.WhsCode);
        var price = GetItemPrice(data.ItemCode, data.DefUoM, 1, $(this).find(".txt-whs-code").val());
        console.log(price)
        $(this).find(".txt-line-price").val(To2Digit(price));


        i++;

    bindRowCalculation();
    formatInputToTwoDigits();
    $(".txt-item-name").trigger('input');
    CalculateDocTotal();

}
function GetSObySalesman() {
    var DocNum = "";
    var docdate = $(".txt-doc-date").val().split("-")[2] + $(".txt-doc-date").val().split("-")[1] + $(".txt-doc-date").val().split("-")[0];
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-sonum?salescode=' + $(".combobox-sale-emp option:selected").data("code") + "&docdate=" + docdate ,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            DocNum = data.docnum;
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
    return DocNum;
}
function GetItemStock(itemcode, UomEntry, whscode) {
    var price = 0;
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-item-stock?itemcode=' + itemcode + "&uomentry=" + UomEntry + "&whscode=" + whscode,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            price = data.price;
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
    return price;
}
function GetItemPrice(itemcode, UomEntry, PriceListCode, whscode) {
    var price = 0;
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-item-price?itemcode=' + itemcode + "&uomentry=" + UomEntry + "&pricelist=" + $(".def_pricelist").val() +"&whscode="+whscode,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            price=data.price;
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
    return price;
}
function GetItemUOM(itemcode, defEntry) {
    var combobox = "";
    var disable = $("#txt-docentry").val() > 0 ? "disabled" : "";
    var readonly = $("#txt-docentry").val() > 0 ? "readonly" : "";
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url:baseUrl+ '/dms/get-uom-by-item?itemcode='+itemcode,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            var option = "";
            for (var x of data.data) {
                //alert(defEntry);
                if (x.UomEntry == defEntry) {
                    option += `<option value="${x.UomEntry}" selected>${x.UomName}</option>`;
                }
                else {
                    option += `<option value="${x.UomEntry}">${x.UomName}</option>`;
                }
            }
            combobox = `<select class="td-input txt-uom-code">
                    ${option}
                </select>`;
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
    return combobox;
}
function GetSalesmanbyCardCode(cardcode) {
    $(".combobox-sale-emp").empty();
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-saleman-cardcode?CardCode=' + cardcode,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            var option = `<option value=""></option>`
            for (var x of data.data) {
                option += `<option value="${x.SlpCode}" data-whs='${x.U_Whs}' data-code='${x.U_SalesCode}' data-sales-type='${x.SALType}'>${x.SalesName}</option>`
            }
            $(".combobox-sale-emp").append(option);
        },
        error: function (error) {
            ShowError("close", error.responseText);
        }
    });
    return combobox;
}
function GetContactPerson(cardcode) {
    $(".txt-contact-person").empty();
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-contact-person?CardCode=' + cardcode,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            var option = "";
            for (var x of data.data) {
                option += `<option value="${x.CntctCode}">${x.Name}</option>`
            }
            $(".txt-contact-person").append(option);
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
    return combobox;
}
function RemoveTRSO(index) {
    if (index >= 0 && index < item_list.length) {
        // Remove the item from the list
        item_list.splice(index, 1);
        promotionRun = false;

        // Re-render the table after removal
        //renderItemTable();
        GetWhsList();
        renderItemTable();

        // Recalculate totals
        CalculateDocTotal();
    }
}
function bindRowCalculation() {

    // Prevent duplicate binding
    $(document).off('input change', '.txt-line-qty, .txt-uom-code, .txt-whs-code, .txt-line-price');

    $(document).on('input change', '.txt-line-qty, .txt-uom-code, .txt-whs-code, .txt-line-price', function () {

        const row = $(this).closest('tr');
        const rowIndex = row.index();

        const item = item_list[rowIndex];

        const qty = parseFloat(row.find('.txt-line-qty').val()) || 0;
        let price = parseFloat(row.find('.txt-line-price').val()) || 0;
        const discount = parseFloat(row.find('.txt-line-discount').val()) || 0;

        const itemCode = row.find('.txt-item-code').val();
        const uom = row.find('.txt-uom-code').val();
        const whs = row.find('.txt-whs-code').val();

        // ===== Stock always refresh =====
        const stock = GetItemStock(itemCode, uom, whs);
        row.find('.txt-line-instock').val(To2Digit(stock));

        // ===== Price reload rules =====
        if (item.SaleType !== "FOC") {

            // New rows ALWAYS get price once
            if (item.IsNew === true) {

                price = GetItemPrice(itemCode, uom, 1, whs);
                row.find('.txt-line-price').val(To2Digit(price));
                item.IsNew = false; // reset after first pricing
            }
            // Reload price only when UOM or WHS changes
            else if (
                $(this).hasClass("txt-uom-code") ||
                $(this).hasClass("txt-whs-code")
            ) {
                price = GetItemPrice(itemCode, uom, 1, whs);
                row.find('.txt-line-price').val(To2Digit(price));
            }
            // If user edits price manually → keep it
        }
        else {
            price = 0;
            row.find('.txt-line-price').val(To2Digit(0));
        }

        // ===== Calculate line total =====
        let total = qty * price;

        if (item.PromotionType === "Discount_Percentage") {

            const discountAmount = (total * discount) / 100;
            row.find('.txt-line-discount-amt').val(To2Digit(discountAmount));
            total -= discountAmount;

        } else if (item.PromotionType === "Discount_Amount") {

            const discountAmount = parseFloat(row.find('.txt-line-discount-amt').val()) || 0;
            total -= discountAmount;

        } else {

            const discountAmount = (total * discount) / 100;
            row.find('.txt-line-discount-amt').val(To2Digit(discountAmount));
            total -= discountAmount;
        }

        row.find('.txt-line-total').val(To2Digit(total));

        // ===== Sync back to item_list =====
        item.Quantity = qty;
        item.UnitPrice = price;
        item.DisPer = discount;
        item.DisAmount = parseFloat(row.find('.txt-line-discount-amt').val()) || 0;
        item.LineTotal = total;
        item.UomEntry = uom;
        item.WhsCode = whs;

        CalculateDocTotal();
    });
}


function To2Digit(value) {
    if (isNaN(value)) {
        value = "0.00";
    }
    value = parseFloat(value) || 0;
    //return value.toFixed(2);
    return value.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}
function formatInputToTwoDigits() {
    $(document).on('change', '.txt-line-qty,.txt-line-price', function () {
       // alert("formatInputToTwoDigits");
        const value = $(this).val();
        var formattedValue = 0;
        if ($(this).hasClass("txt-line-qty") == true) {
            if (isNaN(value)) {
                formattedValue = 0;
            }
            formattedValue = parseFloat(value) || 0;
            $(this).val(formattedValue);
        }
        else {
            // Use the conversion function
            formattedValue = To2Digit(value);

            // Set the formatted value back to the input field
            $(this).val(formattedValue);
        }
        CalculateDocTotal();
    });
}
function CalculateDocTotal() {
    var subtotalbefore = 0;
    for (var x of item_list) {
         subtotalbefore = subtotalbefore + x.LineTotal;

    }

    $(".txt-sub-total-before").val(To2Digit(subtotalbefore));
    var docdis = parseFloat($(".txt-doc-discount").val())||0;

    $(".txt-doc-discount").val(To2Digit(docdis));

    var subtotalafter = subtotalbefore - parseFloat(To2Digit((subtotalbefore * docdis) / 100));

    $(".txt-sub-total-after").val(To2Digit(subtotalafter));
    var vatamount = 0;
    if ($(".combobox-taxtype").val() === "TAX") {
        vatamount = (subtotalafter * 10) / 100; //VAT 10%;
    }
    $(".txt-vat-amount").val(To2Digit(vatamount));
    var doctotal = subtotalafter + vatamount;
    $(".txt-doc-total").val(To2Digit(doctotal));
}
function GetWhsList() {
    var combobox = "";
    var disable = $("#txt-docentry").val() > 0 ? "disabled" : "";
    var salescode = $(".combobox-sale-emp option:selected").val();
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-warehouse-list',
        data: { salescode: salescode, DocEntry: $("#txt-docentry").val() },
        async: false,
        beforeSend: function () { showLoading() },  // ← Remove the 5!
        complete: function () { hideLoading(); },
        success: function (data) {
            var option = "";
            var whs = $(".combobox-sale-emp option:selected").data("whs");
            localStorage.setItem("whslist", JSON.stringify(data.data));
            for (var x of data.data) {
                if (String(x.WhsCode).trim() === String(whs).trim()) {
                    option += `<option value="${x.WhsCode}" selected>${x.WhsName}</option>`
                } else {
                    option += `<option value="${x.WhsCode}">${x.WhsName}</option>`
                }
            }
            combobox = `<select class='td-input txt-whs-code'>
                            ${option}
                        </select>`;
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
    return combobox;
}
//function GetWhsList() {
//    var combobox = "";
//    var disable = $("#txt-docentry").val() > 0 ? "disabled" : "";
//    var readonly = $("#txt-docentry").val() > 0 ? "readonly" : "";
//    var salescode = $(".combobox-sale-emp option:selected").val();

//    callApi({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'GET',
//        url: baseUrl + '/dms/get-warehouse-list',
//        data: { salescode: salescode, DocEntry:$("#txt-docentry").val() },
//        async: false,
//        beforeSend: function () {
//            showLoading()
//        },
//        complete: function () {
//            hideLoading();
//        },
//        success: function (data) {
//            var option = "";
//           //// var selected = $(".combobox-sale-emp").val();
//            //var whs = $(".combobox-sale-emp option:selected").data("whs");
//            localStorage.setItem("whslist", JSON.stringify(data.data));
//            //for (var x of data.data) {
//            //    if (String(x.WhsCode).trim() === String(whs).trim()) {
//            //        option += `<option value="${x.WhsCode}" selected>${x.WhsName}</option>`
//            //    }
//            //    else {
//            //        option += `<option value="${x.WhsCode}">${x.WhsName}</option>`
//            //    }
//            //}
//            ////console.log("Warehouse:" + option);

//            //combobox = `<select class='td-input txt-whs-code'>
//            //                    ${option}
//            //                </select>`;
//        },
//        error: function (error) {
//            ShowError( error.responseText);
//        }
//    });
//    return combobox;
//}
function ApproveReject(Status) {
    var docEntry = $("#txt-docentry").val();
    if (!docEntry) {
        ShowWarning("Sale order not found.");
        return;
    }

    // Show confirm alert before proceeding
    ShowConfirmedAlert(`Are you sure you want to ${Status} this sale order?`, function() {
        // This runs only if user clicks OK
        callApi({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: baseUrl + '/dms/soaction/' + docEntry + "/" + Status,
            async: false,
            beforeSend: function () {
                showLoading();
            },
            complete: function () {
                hideLoading();
            },
            success: function (data) {
                ShowReloadAlert("Your sale order is already " + Status);
            },
            error: function (error) {
                console.log(error);
                ShowError(error.responseText || "Something went wrong.");
            }
        });
    });
}

function ReIntegrateSO() {
    console.log($("#txt-docentry").val())
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/integrate-so/' + $("#txt-docentry").val(),
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            ShowReloadAlert( "Click for re-intergrate success.");
        },
        error: function (error) {
            console.log(error);
            ShowError(error.responseText);
        }
    });
}
function RunPromotion() {
    var header = {
        DocEntry: $(".def_pricelist").val(),
        CardCode: $(".txt-cardcode").val(),
        DocDate: $(".txt-doc-date").val().split("-")[2] + "-" +
            $(".txt-doc-date").val().split("-")[1] + "-" +
            $(".txt-doc-date").val().split("-")[0],
    };

    var detail = item_list.filter(x => x.SaleType == "SAL" && x.LineStatus!="Removed");

    console.log(item_list);

    if (header.CardCode === "") {
        ShowWarning("Customer cannot be blank!");
        return;
    }
    if (detail.length === 0) {
        ShowWarning("Item cannot be blank!");
        return;
    }

    var order = { Header: header, Detail: detail };

    item_list.forEach((data) => {
        data.ProEntry = "-1",
        data.ProLine = "-1",
        data.PromotionType = ""
    });

    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/run-promotion',
        data: JSON.stringify(order),
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            console.log("----Run Promotion");
            console.log(data.data);
            console.log("----Run Promotion");

            (data.data||[]).forEach(x => {
                if (x.GetProType === "DIS") {
                    // Update all matching SAL items
                    item_list.forEach(item => {
                        if (item.ItemCode === x.Item && item.SaleType === "SAL" && item.UomEntry == x.UoMEntry && item.Quantity==x.Qty) {
                            item.DisPer = x.LineDisPer || 0;
                            item.DisAmount = x.LineDisAmt || 0;
                            if (x.PromotionType == "Discount_Percentage") {
                                item.LineTotal = (item.UnitPrice * item.Quantity) - ((item.UnitPrice * item.Quantity) * (x.LineDisPer / 100));//15%
                            }
                            else {
                                item.LineTotal = (item.UnitPrice * item.Quantity) - x.LineDisAmt || 0 ;//15%
                            }
                            //alert((item.UnitPrice * item.Quantity) - x.LineDisAmt || 0);
                            item.ProType = "DIS";
                            item.IsPromotion = "Yes";
                            item.SaleType = "SAL",
                            item.ProEntry = x.ProEntry,
                            item.ProLine = x.ProLineNum,
                            item.PromotionType = x.PromotionType;
                        }
                    });
                }
                if (x.GetProType === "FOC") {
                    // Check if FOC item already exists
                    const exists = item_list.some(i => i.ItemCode === x.Item && i.SaleType === "FOC");
                    if (!exists) {
                        const obj = {
                            LineNum: -1,
                            ItemCode: x.Item,
                            ItemName: x.ItemName || "",
                            defEntry:-1,
                            Quantity: x.Qty || 1,
                            UnitPrice: 0,
                            DisPer: 0,
                            DisAmount: 0,
                            LineTotal: 0,
                            UomEntry: x.UoMEntry || -1,
                            WhsCode: "",
                            SaleType: "FOC",
                            ProEntry: x.ProEntry || -1,
                            ProLine: x.ProLineNum || -1,
                            ProType: "FOC",
                            UoMName: x.UoM,
                            IsPromotion: "Yes",
                            WhsCode: x.WhsCode,
                            WhsName: x.WhsName,
                            PromotionType: "FOC",
                            LineStatus: "Active"
                        };
                        item_list.push(obj);
                    }
                }
                $(".txt-doc-discount").val(To2Digit(x.DocDisPer || 0));
            });
            console.log(item_list);
            //renderItemTable();
            GetWhsList();
            renderItemTable();
            if (data.data.length > 0) {
                $(".txt-doc-discount").val(To2Digit(data.data[0].DocDisPer));
                $(".txt-doc-dis-per-label").val("Discount(" + To2Digit(data.data[0].DocDisPer) + "%)");
                var subtotal = $(".txt-sub-total-before").val().replace(",", "");
                var disamount = subtotal * (data.data[0].DocDisPer / 100);
                $(".txt-doc-discount-amount").val(To2Digit(disamount));
            }
            else {
                $(".txt-doc-dis-per-label").val("Discount(%)");
                $(".txt-doc-discount").val("0.00");
                $(".txt-doc-discount-amount").val("0.00");
            }
            ShowSuccess("Promotion calculation is completed");
        },
        error: function (error) {
            ShowError( error);
        }
    });
}
function ManagerUpdateSOGeneralBP(){
    var header = {
        DocEntry: $("#txt-docentry").val(),
        SubTotal: returnstringvalue(parseFloat($(".txt-sub-total-before").val())),
        DisPer: returnstringvalue(parseFloat($(".txt-doc-discount").val())),
        DisAmount: returnstringvalue(parseFloat($(".txt-doc-discount-amount").val())),
        AfterDis: returnstringvalue(parseFloat($(".txt-sub-total-after").val())),
        VATAmount: returnstringvalue(parseFloat($(".txt-vat-amount").val())),
        Total: returnstringvalue(parseFloat($(".txt-doc-total").val())),
    };
    var detail = [];
    var hasZeroQty = false;
    $(".table-so tbody tr").each(function () {
        var row = $(this).closest("tr");
        var qty = parseFloat(row.find(".txt-line-qty").val()) || 0;
        if (qty === 0) {
            hasZeroQty = true;
        }
        var obj = {
            "DocEntry": -1,
            "LineNum": row.data("line-num"),
            "ItemCode": row.find(".txt-item-code").val(),
            "ItemName": row.find(".txt-item-name").val(),
            "UoMEntry": row.find(".txt-uom-code").val(),
            "Quantity": returnstringvalue(row.find(".txt-line-qty").val()),
            "UnitPrice": returnstringvalue(row.find(".txt-line-price").val()),
            "DisPer": returnstringvalue(row.find(".txt-line-discount").val()),
            "DisAmount": returnstringvalue(row.find(".txt-line-discount-amt").val()), //returnstringvalue((parseFloat(row.find(".txt-line-qty").val()) * parseFloat(row.find(".txt-line-price").val()) * parseFloat(row.find(".txt-line-discount").val()))/100),
            "LineTotal": returnstringvalue(row.find(".txt-line-total").val()),
            "WhsCode": row.find(".txt-whs-code").val(),
            "RefLineNum": (row.data("line-status") == "Active" ? 0 : row.data("line-num")),
            "ProCode": row.data("line-pro-entry"),
            "ProLineNo": row.data("line-pro-line"),
            "PromotionType": row.data("line-pro-type"),
            "SaleType": row.data("line-sal-type")
        }
        detail.push(obj);
    });
    var order = {
        Header: header,
        Detail: detail,
    };
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/manager-save-so',
        data: JSON.stringify(order),
        async: true,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            ShowReloadAlert("Save Success.");
        },
        error: function (error) {
            ShowError(error);
        }
    });
}
function UpdateSOGeneralBP() {
    //if ($(".txt-cardcode").val() == "") {
    //    ICCAlert("close", "Customer cannot be blank!");
    //    return;
    //}
    //var header = {
    //    DocEntry: $("#txt-docentry").val(),
    //    CardCode: $(".txt-cardcode").val(),
    //    CardName: $(".txt-cardname").val(),
    //    DelAddress: $(".txt-address").val(),
    //};
    //var order = {
    //    Header: header,
    //};
    //callApi({
    //    contentType: 'application/json; charset=utf-8',
    //    dataType: 'json',
    //    type: 'POST',
    //    url: baseUrl + '/dms/update-so',
    //    data: JSON.stringify(order),
    //    async: true,
    //    beforeSend: function () {
    //        showLoading()
    //    },
    //    complete: function () {
    //        hideLoading();
    //    },
    //    success: function (data) {
    //        ICCAlert("reload", "Save Success.");
    //    },
    //    error: function (error) {
    //        ICCAlert("close", error);
    //    }
    //});
    SaveSO();
}
function SaveSO() {
    var header = {
        DocEntry: $("#txt-docentry").val(),
        CardCode: $(".txt-cardcode").val(),
        CardName: $(".txt-cardname").val(),
        ContactPer: $(".txt-contact-person").val(),
        DelAddress: "",
        DocDate: $(".txt-doc-date").val().split("-")[2] + "-" + $(".txt-doc-date").val().split("-")[1] + "-" + $(".txt-doc-date").val().split("-")[0],
        DueDate: $(".txt-delivery-date").val().split("-")[2] + "-" + $(".txt-delivery-date").val().split("-")[1] + "-" + $(".txt-delivery-date").val().split("-")[0],
        TaxDate: $(".txt-tax-date").val().split("-")[2] + "-" + $(".txt-tax-date").val().split("-")[1] + "-" + $(".txt-tax-date").val().split("-")[0],
        SubTotal: returnstringvalue(parseFloat($(".txt-sub-total-before").val())),
        DisPer: returnstringvalue(parseFloat($(".txt-doc-discount").val())),
        DisAmount: parseFloat($(".txt-doc-discount").val()) * parseFloat($(".txt-sub-total-before").val()) / 100,
        AfterDis: returnstringvalue(parseFloat($(".txt-sub-total-after").val())),
        VATAmount: returnstringvalue(parseFloat($(".txt-vat-amount").val())),
        Total: returnstringvalue(parseFloat($(".txt-doc-total").val())),
        SalesCode: UserData.Code,
        Remark: $(".txt-remark").val(),
        SalesCode: $(".combobox-sale-emp").val(), //UserData.Code,
        Remark: $(".txt-remark").val(),
        VATType: $(".combobox-taxtype").val(),
    };
    var detail = [];
    var hasZeroQty = false;
    $(".table-so tbody tr").each(function () {
        var row = $(this).closest("tr");
        var qty = parseFloat(row.find(".txt-line-qty").val()) || 0;
        if (qty === 0) {
            hasZeroQty = true;
        }
        var obj = {
            "DocEntry": -1,
            "LineNum": row.data("line-num"),
            "ItemCode":row.find(".txt-item-code").val(),
            "ItemName": row.find(".txt-item-name").val(),
            "UoMEntry": row.find(".txt-uom-code").val(),
            "Quantity": returnstringvalue(row.find(".txt-line-qty").val()),
            "UnitPrice": returnstringvalue(row.find(".txt-line-price").val()),
            "DisPer": returnstringvalue(row.find(".txt-line-discount").val()),
            "DisAmount": returnstringvalue(row.find(".txt-line-discount-amt").val()), //returnstringvalue((parseFloat(row.find(".txt-line-qty").val()) * parseFloat(row.find(".txt-line-price").val()) * parseFloat(row.find(".txt-line-discount").val()))/100),
            "LineTotal": returnstringvalue(row.find(".txt-line-total").val()), 
            "WhsCode": row.find(".txt-whs-code").val(),
            "RefLineNum": (row.data("line-status") == "Active" ? 0: row.data("line-num")),
            "ProCode": row.data("line-pro-entry"),
            "ProLineNo": row.data("line-pro-line"),
            "PromotionType": row.data("line-pro-type"),
            "SaleType": row.data("line-sal-type")
        }
        detail.push(obj);
    });
    if (hasZeroQty) {
        ShowWarning("Some items have quantity 0. Please update before proceeding.");
        return false; 
    }
    if (header.CardCode==="") {
        ShowWarning("Customer cannot be blank!");
        return;
    }
    if (header.SalesCode === "") {
        ShowWarning( "Sales Employee cannot be blank!");
        return;
    }
    if (header.VATType === "") {
        ShowWarning("VAT Type cannot be blank!");
        return;
    }
    if (detail.length === 0) {
        ShowWarning( "Item cannot be blank!");
        return;
    }
    //if (promotionRun == false) {
    //    ShowWarning("You need to click on Run Promotion before save");
    //    return;
    //}
    var order = {
        Header: header,
        Detail: detail,
    };
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/save-so',
        data: JSON.stringify(order),
        async: true,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            ShowReloadAlert("Save Success.");
        },
        error: function (error) {
            ShowError(error);
        }
    });
}
function GetSODetail(key,salstype) {
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-so-detail/' + key,
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            var header = data.data.header;
            var detail = data.data.detail;
            //Set Header
            if (header?.CardCode == "GN00001") {
                $(".txt-cardcode").val("");
                $(".txt-cardname").val("");
                $(".txt-contact-person").val("");
                $(".txt-address").val("");
            }
            else {
                $(".txt-cardcode").val(header?.CardCode);
                $(".txt-cardname").val(header?.CardName);
                $(".txt-contact-person").val(header?.ContactPer);
                GetContactPerson(header?.CardCode);
                $(".txt-address").val(header?.DelAddress);
            }
            
            $(".txt-taxid").val(header?.LicTradNum);
            $(".txt-doc-num").val(header?.DocNo);
            $(".txt-doc-date").val(convertDateFormat(header?.DocDate));
            $(".txt-delivery-date").val(convertDateFormat(header?.DueDate));
            $(".txt-tax-date").val(convertDateFormat(header?.TaxDate));
            $(".txt-sub-total-before").val(To2Digit(header?.SubTotal));
            $(".txt-doc-discount").val(To2Digit(header?.DisPer));
            

            $(".txt-doc-dis-per-label").val("Discount(" + To2Digit(header?.DisPer) + "%)");
            $(".txt-doc-discount-amount").val(To2Digit(header?.DisAmount));

            $(".txt-sub-total-after").val(To2Digit(header?.AfterDis));
            $(".txt-vat-amount").val(To2Digit(header?.VATAmount));
            $(".txt-doc-total").val(To2Digit(header?.Total));
            $(".txt-remark").val(header?.Remark);
            $(".txt-sap-no").val(header?.SAPDocEntry);

            $(".def_pricelist").val(header?.DefPriceListCode);

            //console.log(detail);
            //Set Detail
            item_list = detail.map(obj => ({
                LineNum: obj.LineNum,
                ItemCode: obj.ItemCode,
                ItemName: obj.ItemName,
                defEntry: obj.UoMEntry,
                Quantity: obj.Quantity,
                UnitPrice: obj.UnitPrice,   // keep DB price
                DisPer: obj.DisPer,
                DisAmount: obj.DisAmount,
                LineTotal: obj.LineTotal,
                UomEntry: obj.UoMEntry,
                WhsCode: obj.WhsCode,
                SaleType: obj.SaleType,
                ProEntry: obj.ProCode,
                ProLine: obj.ProLineNo,
                PromotionType: obj.PromotionType,
                ProType: "",
                UoMName: "",
                IsPromotion: "",
                WhsName: "",
                LineStatus: "Active",
                IsNew: false   // 👈 EXISTING LINE
            }));


            //renderItemTable();
            GetWhsList();
            renderItemTable();
            console.log(header?.DocStatus)
            header?.DocStatus === "C" ? $("#btn-cancel-so").hide() : $("#btn-cancel-so").show();
            if (header?.SAPSyncStatus == "Completed") {
                $("#btn-integrate-so").hide()
                $("#btn-cancel-so").hide();
            }
            else {
                $("#btn-integrate-so").show();
                $("#btn-cancel-so").show()
            }
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}
function GetPromotionDetail(key) {
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/get-promotion-detail/' + key,
        data: JSON.stringify(),
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            var header = data.data.header;
            var detail = data.data.detail;
            //Set Header
            $(".txt-promo-code").val(header.PrincipleCode);
            $(".txt-promo-description").val(header.PrincipleDesc);
            $(".txt-promo-form-date").val(convertDateFormat(header.FDate));
            $(".txt-promo-status").val(header.DocStatus);
            $(".txt-promo-to-date").val(convertDateFormat(header.TDate));
            //Set Detail
            var tr = "";
            $(".table-so tbody").empty();
            for (var x of detail) {
                tr += `<tr>
                                <td>${x.ItemGroupName ?? ""}</td>
                                <td>${x.PackType ?? ""}</td>
                                <td>${x.PromotionGroup ?? ""}</td>
                                <td>${x.CardName ?? ""}</td>
                                <td>${x.BPChannelName}</td>
                                <td>${x.CardName}</td>
                                <td>${x.PromotionType}</td>
                                <td align="right">${To2Digit(x.BuyQty)}</td>
                                <td align="right">${x.BuyUoM}</td>
                                <td align="right">${To2Digit(x.BuyAmt)}</td>
                                <td align="right">${To2Digit(x.FOCQty)}</td>
                                <td>${x.FOCUoM ?? ""}</td>
                                <td align="right">${To2Digit(x.DisPer)}</td>
                                <td align="right">${To2Digit(x.DisAmt)}</td>
                                <td align="right">${x.Remark}</td>
                            </tr>`;
            }
            $(".table-so tbody").append(tr);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}
function convertDateFormat(dateString) {
    // Parse the date string into a Date object
    const date = new Date(dateString);

    // Extract day, month, and year
    const day = String(date.getDate()).padStart(2, '0'); // Add leading zero if needed
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
    const year = date.getFullYear();

    // Return the formatted date

    if (dateString === null || dateString==="" || dateString===undefined) {
        return "";
    }
    else {
        return `${day}-${month}-${year}`;
    }
}
function formatDate(dateString) {
    // dateString = "yyyy-M-d" or "dd-MM-yyyy"
    var parts = dateString.split("-");
    if (parts.length === 3) {
        var year = parts[0].length === 4 ? parts[0] : parts[2];
        var month = String(parts[1]).padStart(2, "0");
        var day = String(parts[2]).padStart(2, "0");
        return `${year}-${month}-${day}`;
    }
    return dateString;
}
function SearchSOListnone(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = $("#combobox_salesman").val();
    const status = $(".txt-status").val() || "All";
    const fromdate = $(".txt-from-date").val().split('-')[2] + "/" + $(".txt-from-date").val().split('-')[1] + "/" + $(".txt-from-date").val().split('-')[0];
    const todate = $(".txt-to-date").val().split('-')[2] + "/" + $(".txt-to-date").val().split('-')[1] + "/" + $(".txt-to-date").val().split('-')[0];
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/search-so-list-none',
        data: {
            start: start,
            limit: limit,
            status: status,
            type: type,
            search: search,
            salecode: salecode,
            fromdate: fromdate,
            todate: todate
        },
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            console.log(data);
            $(".table-item tbody").empty();
            var tr = "";
            var index = 1;
            for (var x of data.data) {
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td>${x.DocNo}</td>
                                <td>${convertDateFormat(x.DocDate)}</td>
                                <td>${x.CardName}</td>
                                <td>${x.Remark ?? ""}</td>
                                <td>${x.SalesName ?? ""}</td>
                                <td>${x.Reasons ?? ""}</td>

                            </tr>`;
                index++;
            }
            $(".table-item tbody").append(tr);
            //Set Page index
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, SearchSOList);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}
function SearchSOList(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = $("#combobox_salesman").val();
    const status = $(".txt-status").val() || "All";
    const fromdate = $(".txt-from-date").val().split('-')[2] + "/" + $(".txt-from-date").val().split('-')[1] + "/" + $(".txt-from-date").val().split('-')[0];
    const todate = $(".txt-to-date").val().split('-')[2] + "/" + $(".txt-to-date").val().split('-')[1] + "/" + $(".txt-to-date").val().split('-')[0];
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/search-so-list',
        data: {
            start: start,
            limit: limit,
            status: status,
            type: type,
            search: search,
            salecode: salecode,
            fromdate: fromdate,
            todate: todate
        },
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            console.log(data);
            $(".table-item tbody").empty();
            var tr = "";
            var index = 1;
            for (var x of data.data) {
                let color = (x.Source === "App" ? "blue" : "black");
                tr += `<tr style='color:${color}'>
                                <td style='color:${color}'>${x.RowNumber}</td>
                                <td><a href="${baseUrl}/dms/open-sale-order/${x.DocEntry}">${x.AppDocNo}</a></td>
                                <td style='color:${color}'>${convertDateFormat(x.DocDate)}</td>
                                  <td style='color:${color}'>${convertDateFormat(x.DueDate)}</td>
                                <td style='color:${color}'>${x.CardName}</td>
                                <td style='color:${color}'>${x.Remark ?? ""}</td>
                                <td style='color:${color}'>${x.SalesName ?? ""}</td>
                                <td style='color:${color}' align="right">${To2Digit(x.Total)}</td>
                                <td style='color:${color}'>${(x.DocStatus == 'Draft' ? `Pending ${x.NextApprover} to approve` : x.DocStatus)}</td>
                                <td style='color:${color}'>${x.SAPDocNum ?? ""}</td>
                                <td style='color:${color}'>${x.SAPLastError ??""}</td>
                            </tr>`;
                index++;
            }
            $(".table-item tbody").append(tr);
            //Set Page index
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, SearchSOList);
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}
function SearchSOListVAN(pageNumber) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = $("#combobox_salesman").val();
    const status = $(".txt-status").val() || "All";
    const fromdate = $(".txt-from-date").val().split('-')[2] + "/" + $(".txt-from-date").val().split('-')[1] + "/" + $(".txt-from-date").val().split('-')[0];
    const todate = $(".txt-to-date").val().split('-')[2] + "/" + $(".txt-to-date").val().split('-')[1] + "/" + $(".txt-to-date").val().split('-')[0];
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/search-so-van-list',
        data: {
            start: start,
            limit: limit,
            status: status,
            type: type,
            search: search,
            salecode: salecode,
            fromdate: fromdate,
            todate: todate
        },
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            console.log(data);
            $(".table-item tbody").empty();
            var tr = "";
            var index = 1;
            for (var x of data.data) {
                let color = (x.Source === "App" ? "blue" : "black");
                tr += `<tr style='color:${color}'>
                                <td style='color:${color}'>${x.RowNumber}</td>
                                <td><a href="${baseUrl}/dms/open-sale-order/${x.DocEntry}">${x.AppDocNo}</a></td>
                                <td style='color:${color}'>${convertDateFormat(x.DocDate)}</td>
                                <td style='color:${color}'>${x.CardName}</td>
                                <td style='color:${color}'>${x.Remark ?? ""}</td>
                                <td style='color:${color}'>${x.SalesName ?? ""}</td>
                                <td style='color:${color}' align="right">${To2Digit(x.Total)}</td>
                                <!--td>${x.DocStatus == "O" ? "Open" : x.DocStatus == "C" ? "Cancelled" : ""}</td-->
                                <td style='color:${color}' style='color:${color}'>${(x.DocStatus == 'Draft' ? `Pending ${x.NextApprover} to approve` : x.DocStatus)}</td>
                                <td style='color:${color}'>${x.SAPDocNum ?? ""}</td>
                                <td style='color:${color}'>${x.SAPLastError ?? ""}</td>
                            </tr>`;
                index++;
            }
            $(".table-item tbody").append(tr);
            //Set Page index
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, SearchSOListVAN);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}
function CancelSO() {
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/cancel-so/' + $("#txt-docentry").val(),
        async: true,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            ShowReloadAlert( "Sale order was cancelld.");
        },
        error: function (error) {
            ShowError(error.responseText);
        }
    });
}




function DefaultEmpTab() {
    $(".btn-emp-tab").removeClass("active");
    $(".btn-emp-tab-customer").addClass("active");
    $(".box-tab-customer").show();
}





function SaveReason() {
    // Validation
    if ($(".txt-reason-en").val().trim() === "") {
        ShowWarning( "Reason EN cannot be blank!");
        return;
    }
    if ($(".txt-reason-kh").val().trim() === "") {
        ShowWarning("Reason KH cannot be blank!");
        return;
    }

    // Build reason object
    var reason = {
        Code: $(".txt-reason-code").val().trim() || 0,   // fallback to 0 if blank
        ReasonEN: $(".txt-reason-en").val().trim(),
        Reason1: $(".txt-reason-en").val().trim(),
        ReasonKH: $(".txt-reason-kh").val().trim(),
        Status: $(".txt-reason-status").val(),
        CreatedBy: String(UserData?.Code || "-1")  // send as string to match backend                // fallback if UserData.Code is undefined
    };

    console.log("Sending reason:", reason); // Debug payload before sending

    callApi({
        url: baseUrl + '/dms/save-change-reason',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(reason),
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            ShowReloadAlert( data.message || "Reason saved successfully");
        },
        error: function (xhr) {
            let msg = xhr.responseJSON?.message || xhr.responseText || "Unknown error";
            ShowError("Unexpected error: " + msg);
        }
    });
}


function CheckUser(usercode) {
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + `/dms/check-user?usercode=${usercode}`,
        beforeSend: function () {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
        success: function (data) {
            if (data.data.length > 0) {
                ShowError("User Code already exist");
                $(".txt-user-code").val("");
            }
        },
        error: function (error) {
            console.log(error);
            ShowError("Error: " + error.responseText);
        }
    });
}

function GetUserList(pageNumber = 1) {
    var limit = $(".txt-limit").val() || 20;
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const status = $(".txt-status").val() || "All";
    const userType = $(".txt-search-user-type").val() ?? "All";
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'GET',
        url: baseUrl + '/dms/search-user-list',
        data: {
            start: start,
            limit: limit,
            status: status,
            type: type,
            search: search,
            userType:userType
        },
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            var no = 1;
            var tr = "";
            $(".table-user tbody").empty();
            for (var x of data.data) {
                var profile = profile_url + x.Profile;
                tr += `<tr>
                                <td>${no}</td>
                                <td class="td-img"><img class="img-user" src="${x.Profile == null ? NoProfile : baseUrl+profile}" alt="${x.Profile}" /></td>
                                <td class="td-user-code">${x.Code}</td>
                                <td class="td-user-name">${x.Name}</td>
                                <td>${x.SalesName}</td>
                                <td class="td-user-type">${x.IsWebUser}</td>
                                <td>${x.ManagerName}</td>
                                <!--td class="td-user-status">${x.Status == null ? "Active" : "Inactive"}</td-->
                                <td class="td-user-status">
                                    <div class="badge
                                        bg-${x.Status == "Active" ? 'success' : (x.Active == "Inactive" ? 'danger' : 'secondary')}" 
                                        role="alert">
                                        ${x.Status ?? "None"}
                                    </div>
                                </td>
                                <td><div class="btn btn-edit-user" id="btn-edit-user-${x.Code}" style="color:green;"><i class="fa-solid fa-pen-to-square"></i></div></td>
                                <td class="td-device-id" style="display:none" >${x.DeviceID}</td>
                                <td class="td-link-sap" style="display:none">${x.SlpCode}</td>
                                <td class="td-manager" style="display:none">${x.Manager}</td>
                                <td class="td-close-date" style="display:none">${x.IsEndofDay}</td>
                                <td class="td-printer-name" style="display:none">${x.PrinterName}</td>
                                <td class="td-printer-mac" style="display:none">${x.PrinterMac}</td>
                            </tr>`;
                no++;
            }
            $(".table-user tbody").append(tr);
            $(".img-user").each(function () {
               
            }).on("error", function () {
                $(this).attr("src", NoProfile);
            });

            SetUserPermission();
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetUserList);
        },
        error: function (error) {
            console.log(error);
            ShowError(error.responseText);
        }
    });
}
function SaveUser() {
    if ($(".txt-user-code").val() === "") {
        ShowWarning( "User Code cannot be blank!");
        return;
    }
    if ($(".txt-user-name").val() === "") {
        ShowWarning( "User Name cannot be blank!");
        return;
    }
    
    if ($(".txt-user-type").val() === "") {
        ShowWarning( "User Type cannot be blank!");
        return;
    }

    if ($(".txt-user-type").val() === "App" || $(".txt-user-type").val() === "Both") {
        if ($(".txt-device-id").val() === "") {
            ShowWarning( "Device ID is required");
            return;
        }
        if ($(".txt-sales-employee").val() === "-2") {
            ShowWarning("Link SAP Sales Code is required");
            return;
        }
        if ($(".txt-manager").val() === "") {
            ShowWarning( "Please choose manager ID");
            return;
        }
    }

    var password 
    if ($(".txt-user-code").val() != 0) {
        password = IsCheck == true ? $(".txt-user-password").val() : null;
    }
    else {
        password = $(".txt-user-password").val();
    }
    console.log($(".txt-user-password").val());
    var u = {
        "Code": $(".txt-user-code").val(),
        "Name": $(".txt-user-name").val(),
        "Password": password,
        "UserType": "User",
        "IsWebUser": $(".txt-user-type").val(),
        "SlpCode": $(".txt-sales-employee").val(),
        "IsEndofDay": $(".txt-closed-end-of-day").val(),
        "CreatedBy": UserData.Code,
        "Status": $(".txt-user-status").val(),
        "Manager": $(".txt-manager").val(),
        "DeviceID": $(".txt-device-id").val(),
        "PrinterMac": $(".txt-printer-mac").val(),
        "PrinterName": $(".txt-printer-name").val(),
    }
    callApi({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: baseUrl + '/dms/save-user',
        data: JSON.stringify(u),
        async: false,
        beforeSend: function () {
            showLoading()
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            if (data.success == true) {
                UploadFile(data.data.Code, profile_folder);
                ShowReloadAlert("Save User Success.")
            } else {
                ShowError("Error when save User: " + data.message);
            }
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}
function UploadFile(usercode, folder) {
    let formdata = new FormData();
    let fileInput = document.getElementById("profile"); // e.g., <input type="file" id="fileInput" />
    formdata.append("File", fileInput.files[0]);
    formdata.append("UserCode", usercode);
    formdata.append("Folder", folder);
    formdata.append("CreatedBy", UserData.Code);
    callApi({
        type: 'POST',
        url: baseUrl + '/dms/upload-file',
        data: formdata,
        contentType: false,
        processData: false,
        async:false,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            if (data.code === 200) {
                //alert("✅ File uploaded successfully!");
            } else {
                ShowError("Error when saving file: " + data.message);
            }
        },
        error: function (error) {
            ShowError( "An error occurred: " + error.responseText);
        }
    });
}

function SetUserPermission() {
    console.log(UserData)
    
    if (UserData.UserCode !== "Manager") {
        $("#li_admin").hide();

        //$(".btn-add-user").hide();
        //$(".block-user-type").hide();
        //$(".block-user-status").hide();
        //$(".btn-edit-user").css({ "pointer-events": "none", "opacity": 0.5, "cursor": "no-drop" });
        //$("#btn-edit-user-" + UserData.Code).css({ "pointer-events": "", "opacity": 1, "cursor": "pointer" });
    }
    else {
        $("#li_admin").show();
    }
}
function GetBPRequestList(pageNumber) {
    var limit = $(".txt-limit").val();
    const start = (pageNumber - 1) * limit;
    const type = $(".txt-search-by").val() || "All";
    const search = $(".txt-search").val() || "";
    const salecode = $("#combobox_salesman").val();
    const status = $(".txt-status").val() ?? "All";
   
    callApi({
        url: baseUrl + `/dms/get-bp-request-list?start=${start}&limit=${limit}&type=${type}&search=${search}&salecode=${salecode}&status=${status}`,
        type: 'GET',
        dataType: 'json',
        //async: true,
        beforeSend: function () {
            showLoading();
        },
        complete: function () {
            hideLoading();
        },
        success: function (data) {
            $(".table-bp-request-list tbody").empty();
            var tr = "";
            for (var x of data.data) {
                console.log(x)
                tr += `<tr>
                                <td>${x.RowNumber}</td>
                                <td><a href="${baseUrl}/dms/bp-request/${x.DocEntry}">${x.AppCode}</a></td>
                                <td>${x.CardName}</td>
                                <td>${x.CardFName ?? ""}</td>
                                <td>${x.FullAddEN}</td>
                                <td>${x.SlpName ?? ""}</td>
                                <td>${x.Phone1 ?? ""}</td>
                               <td>${longFormatDate(x.CreatedDate)}</td>
                                <td>
                                    <span class="badge bg-${
                                                    x.Status === 'Approved' ? 'success' :     // Green
                                                        x.Status === 'Draft' ? 'warning' :       // Yellow
                                                            x.Status === 'Rejected' ? 'danger' :     // Red
                                                                x.Status === 'Cancelled' ? 'secondary' : // Gray
                                                                    'dark'                                   // fallback
                                                    }">
                                        ${x.Status ?? 'UNKNOWN'}
                                    </span>
                                </td>
                                 <td>${x.SAPDocNum ?? ""}</td>
                                 <td>${(!x.CardCode)? (x.LastError ?? ""):""}</td>
                            </tr>`;
            }
            $(".table-bp-request-list tbody").append(tr);

            // Set pagination
            const totalrow = data.totalrow;
            buildPagination(totalrow, pageNumber, limit, GetBPRequestList);
        },
        error: function (error) {
            ShowError( error.responseText);
        }
    });
}
function pad(n) {
    return n.toString().padStart(2, '0');
}

// 🔹 Short date: dd-MM-yyyy
function sortFormatDate(dateStr) {
    if (!dateStr) return '';

    const d = new Date(dateStr);
    if (isNaN(d)) return '';

    return `${pad(d.getDate())}-${pad(d.getMonth() + 1)}-${d.getFullYear()}`;
}

// 🔹 Long date: dd-MM-yyyy HH:mm:ss
function longFormatDate(dateStr) {
    if (!dateStr) return '';

    const d = new Date(dateStr);
    if (isNaN(d)) return '';

    return `${pad(d.getDate())}-${pad(d.getMonth() + 1)}-${d.getFullYear()} ` +
        `${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
}
// for bp request
// Load Districts by Province
function loadDistricts(proCode, selectedDisCode = "") {
    $("#txt-discode").html('<option value=""></option>');
    $("#txt-comcode").html('<option value=""></option>'); // clear communes when province changes

    if (!proCode) return;

    callApi({
        url:baseUrl+ '/dms/get-district',
        type: 'GET',
        data: { proCode: proCode },
        beforeSend: function () { console.log("Loading districts..."); },
        success: function (data) {
            console.log("Districts:", data);
            $.each(data, function (i, d) {
                $("#txt-discode").append(
                    `<option value="${d.DisCode}">${d.DisENName} - ${d.DisKHName}</option>`
                );
            });

            if (selectedDisCode) {
                $("#txt-discode").val(selectedDisCode);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error loading districts:", error);
        },
        complete: function () { console.log("Districts loaded."); }
    });
}

// Load Communes by District
function loadCommunes(disCode, selectedComCode = "") {
    $("#txt-comcode").html('<option value=""></option>');

    if (!disCode) return;

    callApi({
        url: baseUrl+ '/dms/get-commune',
        type: 'GET',
        data: { disCode: disCode },
        beforeSend: function () { console.log("Loading communes..."); },
        success: function (data) {
            console.log("Communes:", data);
            $.each(data, function (i, c) {
                $("#txt-comcode").append(
                    `<option value="${c.ComCode}">${c.ComENName} - ${c.ComKHName}</option>`
                );
            });

            if (selectedComCode) {
                $("#txt-comcode").val(selectedComCode);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error loading communes:", error);
        },
        complete: function () { console.log("Communes loaded."); }
    });
}

// Load Address by Province + District + Commune
function loadAddress(proCode, disCode, comCode) {
    $("#txt-fulladdress-en").val('');
    $("#txt-fulladdress-kh").val('');

    if (!proCode || !disCode || !comCode) return;

    callApi({
        url: baseUrl+ '/dms/get-address',
        type: 'GET',
        data: { proCode, disCode, comCode },
        beforeSend: function () { console.log("Loading address..."); },
        success: function (data) {
            console.log("Addresses:", data);
            if (data.length > 0) {
                const addr = data[0]; // pick first address
                $("#txt-fulladdress-en").val(addr.AddressEN || '');
                $("#txt-fulladdress-kh").val(addr.AddressKH || '');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error loading address:", error);
        },
        complete: function () { console.log("Address loaded."); }
    });
}

// Events
$(document).ready(function () {
    $("#txt-procode").on("change", function () {
        const proCode = $(this).val();
        loadDistricts(proCode);
        // Load address after change
        const disCode = $("#txt-discode").val();
        const comCode = $("#txt-comcode").val();
        loadAddress(proCode, disCode, comCode);
    });

    $("#txt-discode").on("change", function () {
        const disCode = $(this).val();
        loadCommunes(disCode);
        // Load address after change
        const proCode = $("#txt-procode").val();
        const comCode = $("#txt-comcode").val();
        loadAddress(proCode, disCode, comCode);
    });

    $("#txt-comcode").on("change", function () {
        const comCode = $(this).val();
        const proCode = $("#txt-procode").val();
        const disCode = $("#txt-discode").val();
        loadAddress(proCode, disCode, comCode);
    });
});


function SaveCustomerRequest() {
    var model = {
        DocEntry: $("#txt-docentry").val(),
        CardName: $("#txt-cardname").val(),
        CardFName: $("#txt-cardfname").val(),
        Channel: $("#txt-channel").val(), // make sure you have this field
        GroupCode: $("#txt-group").val(),
        TermCode: $("#txt-term").val(),
        ProCode: $("#txt-procode").val(),
        DisCode: $("#txt-discode").val(),
        ComCode: $("#txt-comcode").val(),
        VilName: $("#txt-village").val(),
        AddressCode: $("#txt-comcode").val(),
        FullAddEN: $("#txt-fulladdress-en").val(),
        FullAddKH: $("#txt-fulladdress-kh").val(),
        VATNo: $("#txt-vatno").val(),
        StreetNo: $("#txt-streetno").val(), // ensure this input exists
        Region: $("#txt-region").val(),     // ensure this input exists
        Phone1: $("#txt-phone1").val(),
        Phone2: $("#txt-phone2").val(),
        Phone3: $("#txt-phone3").val(),
        Email: $("#txt-email").val(),       // ensure this input exists
        HouseNo: $("#txt-houseno").val(),   // ensure this input exists
        DefPriceListCode: $("#txt-pricelist").val(),
        SubGroup: $("#txt-subgroup").val(),
        Grade: $("#txt-grade").val(),
        Region: $("#txt-regional").val()
    };
    if (
        !model.CardName ||
        !model.CardFName ||
        !model.GroupCode ||
        !model.TermCode ||
        !model.ProCode ||
        !model.DisCode ||
        !model.ComCode ||
        !model.VilName ||
        !model.Phone1 ||
        !model.SubGroup ||
        !model.Grade||
        !model.Region
    ) {
        ShowWarning("Please fill all required fields!");
        console.log("Missing fields:", model);
        return;
    }

    callApi({
        url: baseUrl+ '/dms/save-customer-request',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(model),
        beforeSend: function () { console.log("Saving..."); },
        complete: function () { console.log("Save request complete."); },
        success: function (res) {
            if (res.success) {
                ShowReloadAlert("Save Success");
      
            } else {
                ShowError("Save failed: " + res.message);
            }
        },
        error: function (err) {
            console.error("Error saving:", err);
            ShowError("Error saving: " + err.responseText);
        }
    });
}
function ActionBP(action) {
    var model = {
        DocEntry: $("#txt-docentry").val(),
        CardName: $("#txt-cardname").val(),
        CardFName: $("#txt-cardfname").val(),
        Channel: $("#txt-channel").val(), // make sure you have this field
        GroupCode: $("#txt-group").val(),
        TermCode: $("#txt-term").val(),
        ProCode: $("#txt-procode").val(),
        DisCode: $("#txt-discode").val(),
        ComCode: $("#txt-comcode").val(),
        VilName: $("#txt-village").val(),
        AddressCode: $("#txt-comcode").val(),
        FullAddEN: $("#txt-fulladdress-en").val(),
        FullAddKH: $("#txt-fulladdress-kh").val(),
        VATNo: $("#txt-vatno").val(),
        StreetNo: $("#txt-streetno").val(), // ensure this input exists
        Region: $("#txt-region").val(),     // ensure this input exists
        Phone1: $("#txt-phone1").val(),
        Phone2: $("#txt-phone2").val(),
        Phone3: $("#txt-phone3").val(),
        Email: $("#txt-email").val(),       // ensure this input exists
        HouseNo: $("#txt-houseno").val(),   // ensure this input exists
        DefPriceListCode: $("#txt-pricelist").val(),
        SubGroup: $("#txt-subgroup").val(),
        Grade: $("#txt-grade").val(),
    };
    if ((
        !model.CardName ||
        !model.CardFName ||
        !model.GroupCode ||
        !model.TermCode ||
        !model.ProCode ||
        !model.DisCode ||
        !model.ComCode ||
        !model.VilName ||
        !model.Phone1||
        !model.SubGroup ||
        !model.Grade )&&action==="Approved"
    ) {
        ShowWarning("Please fill all required fields and save change first !");
        console.log("Missing fields:", model);
        return;
    }

    if (!action) return;

    const docentry = $("#txt-docentry").val(); 

    if (!docentry) {
        ShowWarning("BP code not found.");
        return;
    }

    // ✅ Replace standard confirm with Alertify modal
    ShowConfirmedAlert(`Are you sure you want to ${action} this BP?`, function () {
        // This runs if user clicks OK
        callApi({
            url: baseUrl + "/dms/customer-request-action",
            type: "POST",
            data: {
                docentry: docentry,
                action: action
            },
            beforeSend: function () {
                console.log("Sending BP action:", action);
                $("#btn-approve-bp, #btn-reject-bp").prop("disabled", true);
            },
            success: function (res) {
                console.log("Response:", res);
                if (res.success) {
                    ShowReloadAlert(res.message || "Action completed successfully.");
                } else {
                    ShowError(res.message || "Action failed.");
                }
            },
            error: function (xhr) {
                console.error("Error:", xhr.responseText);
                ShowError("Something went wrong. Please try again.");
            },
            complete: function () {
                $("#btn-approve-bp, #btn-reject-bp").prop("disabled", false);
            }
        });
    });
}


function ICCAlert(btn, message) {
    $(".container-alert").show(200);
    $(".container-alert").css({ display: "flex" });
    $(".alert-text").text(message ?? "");
    $(".btn-close-alert").hide();
    $(".btn-reload-alert").hide();
    if (btn === "signout") {
        $(".box-btn-sign-out").show();
        $(".box-button").hide();
        $(".btn-close-alert").show();
    }
    if (btn === "close") {
        $(".btn-close-alert").show();
    }
    if (btn === "reload") {
        $(".btn-reload-alert").show();
    }
}
function CloseAlert() {
    $(".container-alert").hide(200);
}
function ReloadAlert() {
    $(".container-alert").hide(200);
    window.location.reload();
}

//---------------------------------page index variable--------------------------------------
var start = 0;
var limit; //= 10;
var currentpage = 1;
var totalpage = 1;
function SetPageIndex(total, type) {
    total = total == 0 ? 1 : total;
    var btn = "";
    if (type === "sap-bp") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="GetBPMasterDataList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="GetBPMasterDataList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="GetBPMasterDataList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="GetBPMasterDataList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    else if (type === "sap-item") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="GetItemMasterDataList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="GetItemMasterDataList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="GetItemMasterDataList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="GetItemMasterDataList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    else if (type === "dms-plan") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="GetPlanTrackingList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="GetPlanTrackingList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="GetPlanTrackingList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="GetPlanTrackingList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    else if (type === "dms-so") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="SearchSOList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="SearchSOList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="SearchSOList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="SearchSOList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    else if (type === "dms-bp") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="SearchBPList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="SearchBPList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="SearchBPList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="SearchBPList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    else if (type === "sap-sale-emp") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="SearchBPList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="SearchBPList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="SearchBPList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="SearchBPList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    else if (type === "sap-promo-tion") {
        btn = `<div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-first"  onclick="GetPromotionList(1)"><i class="fa-solid fa-angles-left"></i></div>
                        <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-previous" onclick="PreviousNext(false,'${type}')"><i class="fa-solid fa-angle-left"></i></div>`;
        for (var i = 1; i <= total; i++) {
            btn += `<div class="btn btn-flat btn-pageindex"  id="btn-pageindex-${i}" onclick="GetPromotionList(${i})">${i}</div>`;
        }
        btn += `<div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')">...</div>
                    <div class="btn btn-flat btn-pageindex lastindex" id="btn-pageindex-${total}" onclick="GetPromotionList(${total})">${total}</div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-next" onclick="PreviousNext(true,'${type}')"><i class="fa-solid fa-angle-right"></i></div>
                    <div class="btn btn-flat btn-pageindex noradius" id="btn-pageindex-last" onclick="GetPromotionList(${total})"><i class="fa-solid fa-angles-right"></i></div>`;
    }
    $(".btn-pageindex-group").empty();
    $(".btn-pageindex-group").append(btn);
    for (var i = 1; i <= total; i++) {
        $("#btn-pageindex-" + i).hide();
        if (i == currentpage) {
            $("#btn-pageindex-" + i).css({ "background-color": "#BDB76B", "font-weight": "bold", "box-zadow": "" });
        }
    }
    var indexgination = Math.ceil(total / 10);
    for (var i = 0; i < indexgination; i++) {
        if (currentpage > 10 * i && currentpage <= 10 * i + 10) {
            for (var j = 10 * i + 1; j <= 10 * i + 10; j++) {
                $("#btn-pageindex-" + j).show();
            }
        }
        if (currentpage > 10 * (indexgination - 1) && currentpage <= 10 * (indexgination - 1) + 10) {
            $(".lastindex").hide();
        }
        //if (offset == data.display) {
        //    $("#btn_next").removeAttr('onclick');
        //}
    }
}
function PreviousNext(isNext,type) {
    if (isNext === true) {
        if (currentpage !== totalpage) {
            currentpage = currentpage + 1;
        }
        start = (currentpage - 1) * limit;
    }
    else {
        if (currentpage !== 1) {
            currentpage = currentpage - 1;
        }
        start = (currentpage - 1) * limit;
    }
    if (type === "sap-bp") {
        GetBPMasterDataList(currentpage);
    }
    if (type === "sap-item") {
        GetItemMasterDataList(currentpage);
    }
    if (type === "dms-plan") {
        GetPlanTrackingList(currentpage);
    }
    if (type === "dms-so") {
        SearchSOList(currentpage);
    }
    if (type === "dms-bp") {
        SearchBPList(currentpage);
    }
    if (type === "sap-promo-tion") {
        GetPromotionList(currentpage);
    }
}
function buildPagination(totalRows, currentPage, limit, onPageChange) {
    const totalPages = Math.ceil(totalRows / limit);
    const pagination = $(".pagination");
    pagination.empty();

    if (totalRows === 0) {
        pagination.append("<span class='text-danger small'>No record found!</span>");
        return;
    }

    // Always show at least 1 page if data exists
    if (totalPages === 0) return;

    const maxPagesToShow = 10;
    const pageBlockStart = Math.floor((currentPage - 1) / maxPagesToShow) * maxPagesToShow + 1;
    const pageBlockEnd = Math.min(pageBlockStart + maxPagesToShow - 1, totalPages);

    // Prev button
    pagination.append(createPageButton("« Prev", currentPage - 1, false, currentPage === 1));

    // Page numbers
    for (let i = pageBlockStart; i <= pageBlockEnd; i++) {
        pagination.append(createPageButton(i, i, i === currentPage));
    }

    // Ellipsis and last page
    if (pageBlockEnd < totalPages) {
        pagination.append(
            $("<button class='btn-pagination btn-ellipsis'>...</button>").click(function () {
                buildPagination(totalRows, pageBlockEnd + 1, limit, onPageChange);
                if (onPageChange) onPageChange(pageBlockEnd + 1);
            })
        );
        pagination.append(createPageButton(totalPages, totalPages, currentPage === totalPages));
    }

    // Next button
    pagination.append(createPageButton("Next »", currentPage + 1, false, currentPage === totalPages));

    function createPageButton(label, page, isActive = false, isDisabled = false) {
        const btn = $(`<button class='btn-pagination'>${label}</button>`);
        if (isActive) btn.addClass("active");
        if (isDisabled) btn.prop("disabled", true).addClass("disabled");

        btn.click(function () {
            if (!isDisabled && onPageChange) {
                onPageChange(page);
            }
        });
        return btn;
    }
}
// Info / Alert
function ShowAlert(msg, title = "Alert") {
    alertify.alert(title, msg).set({ transition: 'flipx'}).show();
}

// Success
function ShowSuccess(msg, title = "Success") {
    alertify.alert(title, msg).set({ transition: 'flipx' }).show();
}

// Error
function ShowError(msg, title = "Error") {
    alertify.alert(title, msg).set({ transition: 'flipx'}).show();
}

// Warning
function ShowWarning(msg, title = "Warning") {
    alertify.alert(title, msg).set({ transition: 'flipx'}).show();
}

// Confirmation alert with callback
function ShowConfirmedAlert(msg, onYes, title = "Confirmation") {
    alertify.confirm(title, msg,
        function () { // OK clicked
            if (typeof onYes === "function") onYes();
        },
        function () { // Cancel clicked or closed
            // Optional: show canceled feedback
            alertify.error("Action canceled");
        }
    ).set({
        'closable': true, // clicking outside counts as Cancel
        'movable': false,
        'pinnable': false
    });
}

// Alert + reload page
function ShowReloadAlert(msg, title = "Information") {
    alertify.alert(title, msg, function () {
        location.reload();
    }).set({ transition: 'flipx' }).show();
}

//New SO condition for easy to learn
