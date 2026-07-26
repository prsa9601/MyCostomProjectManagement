// wwwroot/js/user-alert.js

(function () {
    'use strict';

    // ============================================================
    // CONFIGURATION
    // ============================================================
    const CONFIG = {
        containerId: 'userAlertContainer',
        defaultDuration: 5000,
        maxAlerts: 5,
        animationDuration: 400,
        position: 'top-center',
        icons: {
            success: 'fa-check-circle',
            error: 'fa-times-circle',
            warning: 'fa-exclamation-triangle',
            info: 'fa-info-circle'
        },
        colors: {
            success: '#10b981',
            error: '#ef4444',
            warning: '#f59e0b',
            info: '#3b82f6'
        },
        // پس‌زمینه‌های تیره‌تر برای خوانایی متن سفید
        bgColors: {
            success: 'rgba(16, 185, 129, 0.25)',
            error: 'rgba(239, 68, 68, 0.25)',
            warning: 'rgba(245, 158, 11, 0.25)',
            info: 'rgba(59, 130, 246, 0.25)'
        },
        borderColors: {
            success: 'rgba(16, 185, 129, 0.4)',
            error: 'rgba(239, 68, 68, 0.4)',
            warning: 'rgba(245, 158, 11, 0.4)',
            info: 'rgba(59, 130, 246, 0.4)'
        }
    };

    // ============================================================
    // STATE
    // ============================================================
    let container = null;
    let alertCount = 0;
    let timers = {};

    // ============================================================
    // STATUS CODE MAP (کامل برای سمت کاربر)
    // ============================================================
    const STATUS_MAP = {
        100: { message: 'ادامه دهید (Continue)', type: 'info' },
        101: { message: 'در حال تغییر پروتکل', type: 'info' },
        102: { message: 'در حال پردازش', type: 'info' },
        200: { message: '✅ درخواست با موفقیت انجام شد.', type: 'success' },
        201: { message: '✅ مورد جدید با موفقیت ایجاد شد.', type: 'success' },
        202: { message: '✅ درخواست پذیرفته شد و در حال پردازش است.', type: 'success' },
        204: { message: '✅ درخواست با موفقیت انجام شد (بدون محتوا).', type: 'success' },
        301: { message: 'منبع به صورت دائم انتقال یافته است.', type: 'info' },
        302: { message: 'منبع به صورت موقت انتقال یافته است.', type: 'info' },
        304: { message: 'منبع تغییری نداشته است.', type: 'info' },
        400: { message: '❌ درخواست نامعتبر است. لطفاً اطلاعات را بررسی کنید.', type: 'error' },
        401: { message: '🔒 شما مجوز دسترسی ندارید. لطفاً وارد شوید.', type: 'error' },
        403: { message: '⛔ شما دسترسی به این بخش را ندارید.', type: 'error' },
        404: { message: '🔍 مورد درخواستی یافت نشد.', type: 'error' },
        405: { message: '❌ روش درخواست مجاز نیست.', type: 'error' },
        408: { message: '⏱️ زمان درخواست به پایان رسید. لطفاً مجدداً تلاش کنید.', type: 'warning' },
        409: { message: '⚠️ تضاد در داده‌ها رخ داده است. لطفاً بررسی کنید.', type: 'error' },
        410: { message: '🗑️ منبع درخواستی دیگر موجود نیست.', type: 'error' },
        411: { message: '❌ طول محتوا مشخص نشده است.', type: 'error' },
        413: { message: '📦 حجم داده ارسالی بیش از حد مجاز است.', type: 'error' },
        415: { message: '❌ نوع داده پشتیبانی نمی‌شود.', type: 'error' },
        422: { message: '❌ داده‌های ارسالی نامعتبر است.', type: 'error' },
        429: { message: '⏳ تعداد درخواست‌ها بیش از حد مجاز است. لطفاً کمی صبر کنید.', type: 'warning' },
        500: { message: '💥 خطای داخلی سرور رخ داده است. لطفاً مجدداً تلاش کنید.', type: 'error' },
        501: { message: '❌ سرویس مورد نظر پیاده‌سازی نشده است.', type: 'error' },
        502: { message: '🌐 درگاه سرور پاسخ نامعتبر ارسال کرده است.', type: 'error' },
        503: { message: '🔧 سرویس در دسترس نیست. لطفاً بعداً تلاش کنید.', type: 'warning' },
        504: { message: '⏱️ زمان پاسخ‌گویی سرور به پایان رسید.', type: 'error' }
    };

    // ============================================================
    // CREATE CONTAINER
    // ============================================================
    function getContainer() {
        if (container && document.body.contains(container)) {
            return container;
        }

        container = document.createElement('div');
        container.id = CONFIG.containerId;
        container.style.cssText = `
            position: fixed;
            top: 24px;
            left: 50%;
            transform: translateX(-50%);
            z-index: 999999;
            width: 100%;
            max-width: 520px;
            padding: 0 16px;
            pointer-events: none;
            display: flex;
            flex-direction: column;
            gap: 12px;
            align-items: center;
            box-sizing: border-box;
        `;
        document.body.appendChild(container);
        return container;
    }

    // ============================================================
    // CREATE ALERT ELEMENT (با متن سفید)
    // ============================================================
    function createAlertElement(options) {
        const {
            id,
            type = 'info',
            title = '',
            message = '',
            duration = CONFIG.defaultDuration,
            showProgress = true,
            closable = true
        } = options;

        const alertDiv = document.createElement('div');
        alertDiv.className = `user-alert-item type-${type}`;
        alertDiv.dataset.id = id;
        alertDiv.style.cssText = `
            pointer-events: auto;
            width: 100%;
            padding: 18px 20px;
            border-radius: 18px;
            background: ${CONFIG.bgColors[type] || CONFIG.bgColors.info};
            backdrop-filter: blur(20px);
            -webkit-backdrop-filter: blur(20px);
            box-shadow: 0 12px 40px rgba(0,0,0,0.25), 0 0 0 1px ${CONFIG.borderColors[type] || CONFIG.borderColors.info};
            display: flex;
            align-items: flex-start;
            gap: 14px;
            animation: userAlertSlideDown ${CONFIG.animationDuration}ms cubic-bezier(0.2, 0.9, 0.3, 1);
            position: relative;
            overflow: hidden;
            box-sizing: border-box;
            font-family: 'Segoe UI', system-ui, -apple-system, sans-serif;
            transition: box-shadow 0.3s;
        `;
        // وقتی ماوس روی آلرت می‌رود، سایه را بیشتر می‌کنیم
        alertDiv.onmouseenter = () => {
            alertDiv.style.boxShadow = '0 16px 48px rgba(0,0,0,0.3), 0 0 0 1px ' + (CONFIG.borderColors[type] || CONFIG.borderColors.info);
        };
        alertDiv.onmouseleave = () => {
            alertDiv.style.boxShadow = '0 12px 40px rgba(0,0,0,0.25), 0 0 0 1px ' + (CONFIG.borderColors[type] || CONFIG.borderColors.info);
        };

        // Icon
        const icon = document.createElement('div');
        icon.style.cssText = `
            width: 40px;
            height: 40px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.2rem;
            flex-shrink: 0;
            margin-top: 2px;
            background: rgba(255,255,255,0.12);
            color: ${CONFIG.colors[type] || CONFIG.colors.info};
            box-shadow: 0 0 0 1px rgba(255,255,255,0.1);
        `;
        icon.innerHTML = `<i class="fas ${CONFIG.icons[type]}"></i>`;
        alertDiv.appendChild(icon);

        // Content (همه متن‌ها سفید)
        const content = document.createElement('div');
        content.style.cssText = `
            flex: 1;
            min-width: 0;
        `;
        if (title) {
            const titleEl = document.createElement('div');
            titleEl.style.cssText = `
                font-size: 0.95rem;
                font-weight: 600;
                color: #ffffff;
                margin-bottom: 2px;
                text-shadow: 0 1px 2px rgba(0,0,0,0.1);
            `;
            titleEl.textContent = title;
            content.appendChild(titleEl);
        }
        const msgEl = document.createElement('div');
        msgEl.style.cssText = `
            font-size: 0.9rem;
            color: #ffffff;
            line-height: 1.6;
            word-break: break-word;
            text-shadow: 0 1px 2px rgba(0,0,0,0.1);
            opacity: 0.95;
        `;
        msgEl.textContent = message;
        content.appendChild(msgEl);
        alertDiv.appendChild(content);

        // Close Button (با رنگ سفید برای هماهنگی)
        if (closable) {
            const closeBtn = document.createElement('button');
            closeBtn.style.cssText = `
                background: none;
                border: none;
                color: rgba(255,255,255,0.6);
                cursor: pointer;
                font-size: 1.1rem;
                padding: 4px;
                transition: all 0.2s;
                flex-shrink: 0;
                margin-top: 2px;
                line-height: 1;
                border-radius: 50%;
                width: 32px;
                height: 32px;
                display: flex;
                align-items: center;
                justify-content: center;
            `;
            closeBtn.innerHTML = '<i class="fas fa-times"></i>';
            closeBtn.onmouseenter = () => {
                closeBtn.style.color = '#ffffff';
                closeBtn.style.background = 'rgba(255,255,255,0.12)';
            };
            closeBtn.onmouseleave = () => {
                closeBtn.style.color = 'rgba(255,255,255,0.6)';
                closeBtn.style.background = 'transparent';
            };
            closeBtn.onclick = (e) => {
                e.stopPropagation();
                removeAlert(id);
            };
            alertDiv.appendChild(closeBtn);
        }

        // Progress Bar
        if (showProgress && duration > 0) {
            const progress = document.createElement('div');
            progress.style.cssText = `
                position: absolute;
                bottom: 0;
                left: 0;
                height: 4px;
                background: rgba(255,255,255,0.4);
                border-radius: 0 0 0 18px;
                width: 100%;
                animation: userProgressShrink ${duration}ms linear forwards;
                opacity: 0.6;
            `;
            alertDiv.appendChild(progress);
        }

        return alertDiv;
    }

    // ============================================================
    // INJECT STYLES (با متن سفید)
    // ============================================================
    function injectStyles() {
        const styleId = 'user-alert-styles';
        if (document.getElementById(styleId)) return;

        const style = document.createElement('style');
        style.id = styleId;
        style.textContent = `
            @keyframes userAlertSlideDown {
                0% { opacity: 0; transform: translateY(-25px) scale(0.96); }
                100% { opacity: 1; transform: translateY(0) scale(1); }
            }
            @keyframes userAlertSlideUp {
                0% { opacity: 1; transform: translateY(0) scale(1); }
                100% { opacity: 0; transform: translateY(-25px) scale(0.96); }
            }
            @keyframes userProgressShrink {
                0% { width: 100%; }
                100% { width: 0%; }
            }
            .user-alert-item.removing {
                animation: userAlertSlideUp 350ms cubic-bezier(0.2, 0.9, 0.3, 1) forwards !important;
            }
            .user-alert-item .close-btn:hover {
                transform: rotate(90deg);
            }

            /* Dark mode - تمام متن‌ها سفید هستند، پس نیازی به تغییر نیست */
        `;
        document.head.appendChild(style);
    }

    // ============================================================
    // CORE FUNCTIONS
    // ============================================================
    function generateId() {
        return 'user-alert-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);
    }

    function showAlert(options) {
        const {
            type = 'info',
            title = '',
            message = '',
            duration = CONFIG.defaultDuration,
            showProgress = true,
            closable = true
        } = options;

        if (!message) {
            console.warn('Alert message is required');
            return null;
        }

        const finalDuration = (duration === null || duration === undefined || duration === 0)
            ? CONFIG.defaultDuration
            : duration;

        const container = getContainer();
        injectStyles();

        const id = generateId();
        const alertElement = createAlertElement({
            id,
            type,
            title,
            message,
            duration: finalDuration,
            showProgress,
            closable
        });

        container.appendChild(alertElement);
        alertCount++;

        if (finalDuration > 0) {
            timers[id] = setTimeout(() => {
                removeAlert(id);
            }, finalDuration + CONFIG.animationDuration);
        }

        return id;
    }

    function removeAlert(id) {
        const container = getContainer();
        const alertElement = container.querySelector(`[data-id="${id}"]`);

        if (!alertElement) return;

        if (timers[id]) {
            clearTimeout(timers[id]);
            delete timers[id];
        }

        alertElement.classList.add('removing');

        setTimeout(() => {
            if (alertElement.parentNode) {
                alertElement.parentNode.removeChild(alertElement);
            }
            alertCount--;
        }, CONFIG.animationDuration);
    }

    function clearAll() {
        const container = getContainer();
        const alerts = container.querySelectorAll('.user-alert-item');
        alerts.forEach(alert => {
            const id = alert.dataset.id;
            if (timers[id]) {
                clearTimeout(timers[id]);
                delete timers[id];
            }
            alert.classList.add('removing');
        });

        setTimeout(() => {
            container.innerHTML = '';
            alertCount = 0;
        }, CONFIG.animationDuration);
    }

    function updateConfig(newConfig) {
        Object.assign(CONFIG, newConfig);
        if (newConfig.position) {
            const oldContainer = document.getElementById(CONFIG.containerId);
            if (oldContainer) {
                oldContainer.remove();
                container = null;
                getContainer();
            }
        }
    }

    // ============================================================
    // STATUS CODE HANDLER
    // ============================================================
    function showStatus(statusCode, customMessage, customTitle, duration) {
        if (typeof statusCode === 'object' && statusCode !== null) {
            const opts = statusCode;
            statusCode = opts.statusCode || opts.code;
            customMessage = opts.message;
            customTitle = opts.title;
            duration = opts.duration;
        }

        const statusInfo = STATUS_MAP[statusCode];
        if (!statusInfo) {
            return showAlert({
                type: 'error',
                title: customTitle || '❌ خطای ناشناخته',
                message: customMessage || `خطای ناشناخته با کد ${statusCode}`,
                duration: duration || CONFIG.defaultDuration
            });
        }

        const finalTitle = customTitle || (statusInfo.type === 'success' ? '✅ موفقیت' :
            statusInfo.type === 'error' ? '❌ خطا' :
                statusInfo.type === 'warning' ? '⚠️ هشدار' : 'ℹ️ اطلاعیه');

        return showAlert({
            type: statusInfo.type,
            title: finalTitle,
            message: customMessage || statusInfo.message,
            duration: duration || CONFIG.defaultDuration
        });
    }

    // ============================================================
    // GLOBAL FUNCTIONS
    // ============================================================
    window.showUserAlert = function (message, type, title, duration) {
        if (type === undefined) return showAlert({ message: message });
        if (title === undefined) return showAlert({ message: message, type: type });
        if (duration === undefined) return showAlert({ message: message, type: type, title: title });
        return showAlert({ message: message, type: type, title: title, duration: duration });
    };

    window.showUserStatus = function (statusCode, customMessage, customTitle, duration) {
        return showStatus(statusCode, customMessage, customTitle, duration);
    };

    window.UserAlert = {
        show: showAlert,
        remove: removeAlert,
        clearAll: clearAll,
        config: updateConfig,
        showStatus: showStatus,
        showSuccess: (message, title, duration) =>
            showAlert({ type: 'success', title: title || '✅ موفقیت', message, duration }),
        showError: (message, title, duration) =>
            showAlert({ type: 'error', title: title || '❌ خطا', message, duration }),
        showWarning: (message, title, duration) =>
            showAlert({ type: 'warning', title: title || '⚠️ هشدار', message, duration }),
        showInfo: (message, title, duration) =>
            showAlert({ type: 'info', title: title || 'ℹ️ اطلاعیه', message, duration })
    };

    console.log('✅ User Alert system initialized. All texts are white.');
})();