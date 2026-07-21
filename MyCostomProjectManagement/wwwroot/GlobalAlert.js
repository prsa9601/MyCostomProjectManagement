// wwwroot/js/alert.js

(function () {
    'use strict';

    // ============================================================
    // CONFIGURATION
    // ============================================================
    const CONFIG = {
        containerId: 'alertContainer',
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
            success: '#34d399',
            error: '#ef4444',
            warning: '#fb923c',
            info: '#3b82f6'
        }
    };

    // ============================================================
    // STATE
    // ============================================================
    let container = null;
    let alertCount = 0;
    let timers = {};

    // ============================================================
    // STATUS CODE MAP
    // ============================================================
    const STATUS_MAP = {
        100: { message: 'ادامه دهید (Continue)', type: 'info' },
        101: { message: 'در حال تغییر پروتکل (Switching Protocols)', type: 'info' },
        102: { message: 'در حال پردازش (Processing)', type: 'info' },
        200: { message: 'درخواست با موفقیت انجام شد.', type: 'success' },
        201: { message: 'مورد جدید با موفقیت ایجاد شد.', type: 'success' },
        202: { message: 'درخواست پذیرفته شد و در حال پردازش است.', type: 'success' },
        203: { message: 'اطلاعات معتبر نیستند (Non-Authoritative Information)', type: 'info' },
        204: { message: 'درخواست با موفقیت انجام شد (بدون محتوا).', type: 'success' },
        205: { message: 'بازنشانی محتوا (Reset Content)', type: 'info' },
        206: { message: 'بخشی از محتوا (Partial Content)', type: 'info' },
        207: { message: 'وضعیت چندگانه (Multi-Status)', type: 'info' },
        208: { message: 'از قبل گزارش شده (Already Reported)', type: 'info' },
        226: { message: 'IM استفاده شده (IM Used)', type: 'info' },
        300: { message: 'انتخاب چندگانه (Multiple Choices)', type: 'info' },
        301: { message: 'منبع به صورت دائم انتقال یافته است.', type: 'info' },
        302: { message: 'منبع به صورت موقت انتقال یافته است.', type: 'info' },
        303: { message: 'به روش دیگر مراجعه کنید (See Other)', type: 'info' },
        304: { message: 'منبع تغییری نداشته است.', type: 'info' },
        305: { message: 'استفاده از پروکسی (Use Proxy)', type: 'info' },
        306: { message: '(غیرقابل استفاده) - Switch Proxy', type: 'info' },
        307: { message: 'تغییر مسیر موقت (Temporary Redirect)', type: 'info' },
        308: { message: 'تغییر مسیر دائم (Permanent Redirect)', type: 'info' },
        400: { message: 'درخواست نامعتبر است. لطفاً اطلاعات را بررسی کنید.', type: 'error' },
        401: { message: 'شما مجوز دسترسی ندارید. لطفاً وارد شوید.', type: 'error' },
        402: { message: 'پرداخت لازم است (Payment Required)', type: 'error' },
        403: { message: 'شما دسترسی به این بخش را ندارید.', type: 'error' },
        404: { message: 'مورد درخواستی یافت نشد.', type: 'error' },
        405: { message: 'روش درخواست مجاز نیست.', type: 'error' },
        406: { message: 'پاسخ قابل قبول نیست (Not Acceptable)', type: 'error' },
        407: { message: 'احراز هویت پروکسی لازم است (Proxy Authentication Required)', type: 'error' },
        408: { message: 'زمان درخواست به پایان رسید. لطفاً مجدداً تلاش کنید.', type: 'warning' },
        409: { message: 'تضاد در داده‌ها رخ داده است. لطفاً بررسی کنید.', type: 'error' },
        410: { message: 'منبع درخواستی دیگر موجود نیست.', type: 'error' },
        411: { message: 'طول محتوا مشخص نشده است.', type: 'error' },
        412: { message: 'پیش‌شرط شکست خورده (Precondition Failed)', type: 'error' },
        413: { message: 'حجم داده ارسالی بیش از حد مجاز است.', type: 'error' },
        414: { message: 'URI بیش از حد طولانی است (URI Too Long)', type: 'error' },
        415: { message: 'نوع داده پشتیبانی نمی‌شود.', type: 'error' },
        416: { message: 'محدوده درخواستی قابل ارائه نیست (Range Not Satisfiable)', type: 'error' },
        417: { message: 'انتظار ناموفق (Expectation Failed)', type: 'error' },
        418: { message: 'من یک قوری هستم (I\'m a teapot)', type: 'info' },
        421: { message: 'درخواست به درستی هدایت نشده (Misdirected Request)', type: 'error' },
        422: { message: 'داده‌های ارسالی نامعتبر است (Unprocessable Entity)', type: 'error' },
        423: { message: 'منبع قفل است (Locked)', type: 'error' },
        424: { message: 'وابستگی شکست خورده (Failed Dependency)', type: 'error' },
        425: { message: 'بسیار زود (Too Early)', type: 'warning' },
        426: { message: 'ارتقا لازم است (Upgrade Required)', type: 'info' },
        428: { message: 'پیش‌شرط لازم است (Precondition Required)', type: 'error' },
        429: { message: 'تعداد درخواست‌ها بیش از حد مجاز است. لطفاً کمی صبر کنید.', type: 'warning' },
        431: { message: 'فیلدهای هدر بسیار بزرگ هستند (Request Header Fields Too Large)', type: 'error' },
        451: { message: 'به دلایل قانونی در دسترس نیست (Unavailable For Legal Reasons)', type: 'error' },
        500: { message: 'خطای داخلی سرور رخ داده است. لطفاً مجدداً تلاش کنید.', type: 'error' },
        501: { message: 'سرویس مورد نظر پیاده‌سازی نشده است.', type: 'error' },
        502: { message: 'درگاه سرور پاسخ نامعتبر ارسال کرده است.', type: 'error' },
        503: { message: 'سرویس در دسترس نیست. لطفاً بعداً تلاش کنید.', type: 'warning' },
        504: { message: 'زمان پاسخ‌گویی سرور به پایان رسید.', type: 'error' },
        505: { message: 'نسخه HTTP پشتیبانی نمی‌شود (HTTP Version Not Supported)', type: 'error' },
        506: { message: 'تنوع همچنین مذاکره می‌کند (Variant Also Negotiates)', type: 'error' },
        507: { message: 'فضای ذخیره‌سازی ناکافی (Insufficient Storage)', type: 'error' },
        508: { message: 'حلقه شناسایی شد (Loop Detected)', type: 'error' },
        510: { message: 'تمدید نشده (Not Extended)', type: 'error' },
        511: { message: 'احراز هویت شبکه لازم است (Network Authentication Required)', type: 'error' }
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
                top: 20px;
                left: 50%;
                transform: translateX(-50%);
                z-index: 99999;
                width: 100%;
                max-width: 520px;
                padding: 0 16px;
                pointer-events: none;
                display: flex;
                flex-direction: column;
                gap: 10px;
                align-items: center;
                box-sizing: border-box;
            `;
        document.body.appendChild(container);
        return container;
    }

    // ============================================================
    // CREATE ALERT ELEMENT
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
        alertDiv.className = `alert-item type-${type}`;
        alertDiv.dataset.id = id;
        alertDiv.style.cssText = `
                pointer-events: auto;
                width: 100%;
                padding: 16px 20px;
                border-radius: 16px;
                background: ${getBackgroundColor(type)};
                backdrop-filter: blur(20px);
                -webkit-backdrop-filter: blur(20px);
                box-shadow: 0 8px 40px rgba(0, 0, 0, 0.5);
                display: flex;
                align-items: flex-start;
                gap: 14px;
                border: 1px solid ${getBorderColor(type)};
                animation: alertSlideDown ${CONFIG.animationDuration}ms cubic-bezier(0.2, 0.9, 0.3, 1);
                position: relative;
                overflow: hidden;
                box-sizing: border-box;
            `;

        // Icon
        const icon = document.createElement('div');
        icon.className = 'alert-icon';
        icon.style.cssText = `
                width: 36px;
                height: 36px;
                border-radius: 50%;
                display: flex;
                align-items: center;
                justify-content: center;
                font-size: 1.1rem;
                flex-shrink: 0;
                margin-top: 2px;
                background: ${getIconBackground(type)};
                color: ${CONFIG.colors[type]};
            `;
        icon.innerHTML = `<i class="fas ${CONFIG.icons[type]}"></i>`;
        alertDiv.appendChild(icon);

        // Content
        const content = document.createElement('div');
        content.className = 'alert-content';
        content.style.cssText = `
                flex: 1;
                min-width: 0;
            `;
        if (title) {
            const titleEl = document.createElement('div');
            titleEl.className = 'title';
            titleEl.style.cssText = `
                    font-size: 0.9rem;
                    font-weight: 600;
                    color: #fff;
                    margin-bottom: 2px;
                `;
            titleEl.textContent = title;
            content.appendChild(titleEl);
        }
        const msgEl = document.createElement('div');
        msgEl.className = 'message';
        msgEl.style.cssText = `
                font-size: 0.85rem;
                color: rgba(255, 255, 255, 0.7);
                line-height: 1.5;
                word-break: break-word;
            `;
        msgEl.textContent = message;
        content.appendChild(msgEl);
        alertDiv.appendChild(content);

        // Close Button
        if (closable) {
            const closeBtn = document.createElement('button');
            closeBtn.className = 'close-btn';
            closeBtn.style.cssText = `
                    background: none;
                    border: none;
                    color: rgba(255, 255, 255, 0.2);
                    cursor: pointer;
                    font-size: 1rem;
                    padding: 4px;
                    transition: 0.3s;
                    flex-shrink: 0;
                    margin-top: 2px;
                    line-height: 1;
                `;
            closeBtn.innerHTML = '<i class="fas fa-times"></i>';
            closeBtn.onmouseenter = () => closeBtn.style.color = 'rgba(255,255,255,0.8)';
            closeBtn.onmouseleave = () => closeBtn.style.color = 'rgba(255,255,255,0.2)';
            closeBtn.onclick = (e) => {
                e.stopPropagation();
                removeAlert(id);
            };
            alertDiv.appendChild(closeBtn);
        }

        // Progress Bar
        if (showProgress && duration > 0) {
            const progress = document.createElement('div');
            progress.className = 'progress-bar';
            progress.style.cssText = `
                    position: absolute;
                    bottom: 0;
                    left: 0;
                    height: 3px;
                    background: ${getProgressColor(type)};
                    border-radius: 0 0 0 16px;
                    width: 100%;
                    animation: progressShrink ${duration}ms linear forwards;
                `;
            alertDiv.appendChild(progress);
        }

        return alertDiv;
    }

    // ============================================================
    // HELPER FUNCTIONS
    // ============================================================
    function getBackgroundColor(type) {
        const colors = {
            success: 'rgba(52, 211, 153, 0.06)',
            error: 'rgba(239, 68, 68, 0.06)',
            warning: 'rgba(251, 146, 60, 0.06)',
            info: 'rgba(59, 130, 246, 0.06)'
        };
        return colors[type] || colors.info;
    }

    function getBorderColor(type) {
        const colors = {
            success: 'rgba(52, 211, 153, 0.08)',
            error: 'rgba(239, 68, 68, 0.08)',
            warning: 'rgba(251, 146, 60, 0.08)',
            info: 'rgba(59, 130, 246, 0.08)'
        };
        return colors[type] || colors.info;
    }

    function getIconBackground(type) {
        const colors = {
            success: 'rgba(52, 211, 153, 0.12)',
            error: 'rgba(239, 68, 68, 0.12)',
            warning: 'rgba(251, 146, 60, 0.12)',
            info: 'rgba(59, 130, 246, 0.12)'
        };
        return colors[type] || colors.info;
    }

    function getProgressColor(type) {
        const colors = {
            success: 'rgba(52, 211, 153, 0.2)',
            error: 'rgba(239, 68, 68, 0.2)',
            warning: 'rgba(251, 146, 60, 0.2)',
            info: 'rgba(59, 130, 246, 0.2)'
        };
        return colors[type] || colors.info;
    }

    // ============================================================
    // INJECT STYLES
    // ============================================================
    function injectStyles() {
        const styleId = 'alert-styles';
        if (document.getElementById(styleId)) return;

        const style = document.createElement('style');
        style.id = styleId;
        style.textContent = `
                @keyframes alertSlideDown {
                    0% { opacity: 0; transform: translateY(-30px) scale(0.95); }
                    100% { opacity: 1; transform: translateY(0) scale(1); }
                }
                @keyframes alertSlideUp {
                    0% { opacity: 1; transform: translateY(0) scale(1); }
                    100% { opacity: 0; transform: translateY(-30px) scale(0.95); }
                }
                @keyframes progressShrink {
                    0% { width: 100%; }
                    100% { width: 0%; }
                }
                .alert-item.removing {
                    animation: alertSlideUp 350ms cubic-bezier(0.2, 0.9, 0.3, 1) forwards !important;
                }
                .alert-item .close-btn:hover {
                    transform: rotate(90deg);
                }
            `;
        document.head.appendChild(style);
    }

    // ============================================================
    // CORE FUNCTIONS
    // ============================================================
    function generateId() {
        return 'alert-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);
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

        const container = getContainer();
        injectStyles();

        const id = generateId();
        const alertElement = createAlertElement({
            id,
            type,
            title,
            message,
            duration,
            showProgress,
            closable
        });

        container.appendChild(alertElement);
        alertCount++;

        if (duration > 0) {
            timers[id] = setTimeout(() => {
                removeAlert(id);
            }, duration + CONFIG.animationDuration);
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
        const alerts = container.querySelectorAll('.alert-item');
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
    // STATUS CODE HANDLER (با ورودی‌های متغیر)
    // ============================================================
    function showStatus(statusCode, customMessage, customTitle, duration) {
        // پشتیبانی از پارامترهای متغیر
        // همچنین می‌تواند یک شیء به عنوان اولین پارامتر دریافت کند
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
                title: customTitle || 'خطای ناشناخته',
                message: customMessage || `خطای ناشناخته با کد ${statusCode}`,
                duration: duration || CONFIG.defaultDuration
            });
        }

        const finalTitle = customTitle || (statusInfo.type === 'success' ? 'موفقیت آمیز' :
            statusInfo.type === 'error' ? 'خطا' :
                statusInfo.type === 'warning' ? 'هشدار' : 'اطلاعیه');

        return showAlert({
            type: statusInfo.type,
            title: finalTitle,
            message: customMessage || statusInfo.message,
            duration: duration || CONFIG.defaultDuration
        });
    }

    // ============================================================
    // GLOBAL FUNCTIONS FOR DIRECT INVOKE FROM IJSRuntime
    // ============================================================

    // 1. showAlert(message, type, title, duration)
    //    - type: 'success' | 'error' | 'warning' | 'info' (پیش‌فرض: 'info')
    //    - title: عنوان (پیش‌فرض: بر اساس نوع)
    //    - duration: مدت زمان بر حسب میلی‌ثانیه (پیش‌فرض: 5000)
    window.showAlert = function (message, type, title, duration) {
        // اگر فقط یک پارامتر داده شده (message)
        if (type === undefined) {
            return showAlert({ message: message });
        }
        // اگر دو پارامتر (message, type)
        if (title === undefined) {
            return showAlert({ message: message, type: type });
        }
        // اگر سه پارامتر (message, type, title)
        if (duration === undefined) {
            return showAlert({ message: message, type: type, title: title });
        }
        // چهار پارامتر
        return showAlert({ message: message, type: type, title: title, duration: duration });
    };

    // 2. showStatus(statusCode, customMessage, customTitle, duration)
    window.showStatus = function (statusCode, customMessage, customTitle, duration) {
        return showStatus(statusCode, customMessage, customTitle, duration);
    };

    // ============================================================
    // EXPOSE TO GLOBAL SCOPE (همچنان window.Alert نیز موجود است)
    // ============================================================
    window.Alert = {
        show: showAlert,
        remove: removeAlert,
        clearAll: clearAll,
        config: updateConfig,
        showStatus: showStatus,
        showSuccess: (message, title, duration) =>
            showAlert({ type: 'success', title: title || 'موفقیت آمیز', message, duration }),
        showError: (message, title, duration) =>
            showAlert({ type: 'error', title: title || 'خطا', message, duration }),
        showWarning: (message, title, duration) =>
            showAlert({ type: 'warning', title: title || 'هشدار', message, duration }),
        showInfo: (message, title, duration) =>
            showAlert({ type: 'info', title: title || 'اطلاعیه', message, duration })
    };

    console.log('✅ Alert system initialized. Use showAlert() or window.Alert.show()');
})();

// در یک صفحه یا کامپوننت
//@inject IJSRuntime JSRuntime
//
//private async Task ShowAlerts()
//{
//    // 1. فقط پیام (پیش‌فرض: type=info, title='اطلاعیه', duration=5000)
//    await JSRuntime.InvokeVoidAsync("showAlert", "این یک پیام اطلاعیه است");
//
//    // 2. پیام + نوع
//    await JSRuntime.InvokeVoidAsync("showAlert", "این یک هشدار است", "warning");
//
//    // 3. پیام + نوع + عنوان
//    await JSRuntime.InvokeVoidAsync("showAlert", "این یک خطاست", "error", "خطای سفارشی");
//
//    // 4. پیام + نوع + عنوان + مدت زمان (میلی‌ثانیه)
//    await JSRuntime.InvokeVoidAsync("showAlert", "عملیات موفق!", "success", "تبریک", 3000);
//
//    // 5. نمایش با کد وضعیت (با showStatus)
//    await JSRuntime.InvokeVoidAsync("showStatus", 404);   // فقط کد
//    await JSRuntime.InvokeVoidAsync("showStatus", 404, "موردی یافت نشد!");   // کد + پیام سفارشی
//    await JSRuntime.InvokeVoidAsync("showStatus", 404, "موردی یافت نشد!", "خطای سفارشی");   // کد + پیام + عنوان
//    await JSRuntime.InvokeVoidAsync("showStatus", 404, "موردی یافت نشد!", "خطای سفارشی", 3000);   // همه
//}