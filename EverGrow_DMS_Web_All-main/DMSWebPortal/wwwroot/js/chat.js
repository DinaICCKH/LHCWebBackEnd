"use strict";

//$(document).ready(function () {
//    // ✅ Get user data
//    const UserData = JSON.parse(sessionStorage.getItem("UserData"));
//    if (!UserData || !UserData.Code) {
//        console.error("UserData not found in sessionStorage");
//        return;
//    }

//    const userCode = UserData.Code;
//    const baseUrl = window.location.origin;
//    let notificationCount = 0;

//    // ✅ Initialize SignalR connection
//    const connection = new signalR.HubConnectionBuilder()
//        .withUrl(`${baseUrl}/chathub?usercode=${userCode}`)
//        .withAutomaticReconnect([0, 2000, 5000, 10000])
//        .build();

//    // ✅ Start connection
//    async function startConnection() {
//        try {
//            await connection.start();
//            console.log("✅ SignalR connected");

//            // Get initial count
//            notificationCount = await connection.invoke("GetCurrentNotificationCount");
//            $(".pop-notification-num").text(notificationCount);
//        } catch (err) {
//            console.error("SignalR connection failed:", err);
//            setTimeout(startConnection, 5000);
//        }
//    }

//    startConnection();

//    // ✅ Receive live messages
//    connection.on("ReceiveMessage",async function (user, message) {
//        showToast(user, message);
//        notificationCount++;
//        $(".pop-notification-num").text(notificationCount);
//        // 🔄 Refresh notification list if it's currently visible
//        const $dropdown = $("#notificationList");
//        if ($dropdown.is(":visible")) {
//            try {
//                const notifications = await connection.invoke("GetNotifications");
//                renderNotifications(notifications);
//            } catch (err) {
//                console.error("Error refreshing notifications:", err);
//            }
//        }
//    });

//    // ✅ Update badge count from backend
//    connection.on("UpdateBadge",async function (count) {
//        notificationCount = count;
//        $(".pop-notification-num").text(count);

//        const $dropdown = $("#notificationList");
//        if ($dropdown.is(":visible")) {
//            try {
//                const notifications = await connection.invoke("GetNotifications");
//                renderNotifications(notifications);
//            } catch (err) {
//                console.error("Error refreshing notifications:", err);
//            }
//        }
//    });

//    // ✅ Toast helper
//    function showToast(user, message) {
//        if (!$(".toast-container").length) {
//            $("body").append('<div class="toast-container position-fixed top-0 end-0 p-3"></div>');
//        }

//        const now = new Date();
//        const timeStr = now.toLocaleTimeString();

//        const $toast = $(`
//            <div class="toast align-items-center bg-light border border-1"
//                 role="alert" aria-live="assertive" aria-atomic="true"
//                 data-bs-autohide="true" data-bs-delay="8000">
//                <div class="toast-header bg-white border-bottom">
//                    <strong class="me-auto text-dark">${user}</strong>
//                    <small class="text-muted">${timeStr}</small>
//                    <button type="button" class="btn-close" data-bs-dismiss="toast"></button>
//                </div>
//                <div class="toast-body">${message}</div>
//            </div>
//        `);

//        $(".toast-container").append($toast);
//        const toast = new bootstrap.Toast($toast[0]);
//        toast.show();
//        $toast.on("hidden.bs.toast", () => $toast.remove());
//    }

//    // ✅ Notification dropdown click
//    $(".btn-notfication").click(async function (e) {
//        e.stopPropagation();
//        const $dropdown = $("#notificationList");
//        $dropdown.toggle();

//        // Load notifications when opening
//        if ($dropdown.is(":visible")) {
//            try {
//                const notifications = await connection.invoke("GetNotifications");
//                renderNotifications(notifications);
//            } catch (err) {
//                console.error("Error loading notifications:", err);
//            }
//        }
//    });

//    // ✅ Render notification items
//    function renderNotifications(notifications) {
//        let $dropdown = $("#notificationList");
//        if (!$dropdown.length) {
//            $("body").append(`
//                <div id="notificationList" class="notification-dropdown">
//                    <div class="notification-content"></div>
//                </div>
//            `);
//            $dropdown = $("#notificationList");
//        }

//        const $content = $dropdown.find(".notification-content");
//        $content.empty();

//        if (!notifications || notifications.length === 0) {
//            $content.html('<div class="no-notifications text-center text-muted p-2">No notifications</div>');
//            return;
//        }

//        notifications.forEach(n => {
//            const isUnread = !n.isViewed;
//            const $item = $(`
//                <div class="notification-item ${isUnread ? "unread" : ""}" data-id="${n.notificationId}">
//                    <div class="notif-header d-flex justify-content-between small text-muted mb-1">
//                        <span class="notif-type fw-bold text-primary">${n.type || ""}</span>
//                        <span class="notif-time">${n.timeAgo || ""}</span>
//                    </div>
//                    <div class="notif-message small">${n.message || ""}</div>
//                    ${n.docNo ? `<div class="notif-doc small text-secondary">Doc No: ${n.docNo}</div>` : ""}
//                    ${n.remark ? `<div class="notif-remark small text-muted">${n.remark}</div>` : ""}
//                </div>
//            `);
//            $content.append($item);
//        });

//        // ✅ Click -> mark ONE as read
//        $(".notification-item").off("click").on("click", async function () {
//            const notifId = $(this).data("id");
//            try {
//                await connection.invoke("MarkNotificationRead", notifId);
//                $(this).removeClass("unread");

//                // Update badge number visually
//                const current = parseInt($(".pop-notification-num").text()) || 0;
//                $(".pop-notification-num").text(Math.max(0, current - 1));
//            } catch (err) {
//                console.error("Error marking read:", err);
//            }
//        });
//    }

//    // ✅ Hide dropdown when clicking outside
//    $(document).on("click", function (e) {
//        if (!$(e.target).closest(".notification-box, #notificationList").length) {
//            $("#notificationList").hide();
//        }
//    });
//    // ✅ Clear all notifications (set isViewed = true)
//    $("#clearAll").click(async function () {
//        try {
//            // Call SignalR method to mark all as viewed
//            await connection.invoke("MarkAllNotificationsRead");

//            // Update UI — hide 'unread' highlight and reduce badge count
//            $(".notification-item").removeClass("unread");
//            $(".pop-notification-num").text(0);

//            // Optionally show message inside the dropdown
//            const $container = $("#notificationList .notification-content");
//            $container.empty().append('<div class="text-center text-muted p-3">All notifications cleared</div>');
//        } catch (err) {
//            console.error("Error clearing all notifications:", err);
//        }
//    });

//    // ✅ Reconnect handling
//    connection.onclose(async () => {
//        console.warn("SignalR disconnected, retrying...");
//        await startConnection();
//    });
//});
