(function () {
    var TRAY_CLASS = 'product-image-badges';
    var BADGE_SELECTOR = '.new-product-badge, .show-discount-percentage';

    function productRoot(el) {
        return el.closest('.product-item')
            || el.closest('.item-box')
            || el.closest('.product-essential')
            || el.closest('.product-details-page');
    }

    function hostFor(el) {
        var item = el.closest('.product-item') || el.closest('.item-box');
        if (item) {
            return item.querySelector('.product-item-picture-wrapper')
                || item.querySelector('.picture')
                || item;
        }

        return el.closest('.picture-gallery')
            || el.closest('.gallery')
            || document.querySelector('.product-details-page .picture-gallery, .product-details-page .gallery, .product-essential .picture');
    }

    function trayFor(host) {
        host.classList.add('product-image-badges-host');
        var tray = host.querySelector(':scope > .' + TRAY_CLASS) || host.querySelector('.' + TRAY_CLASS);
        if (!tray) {
            tray = document.createElement('div');
            tray.className = TRAY_CLASS;
            host.appendChild(tray);
        }
        return tray;
    }

    function placeIntoTray(el) {
        var root = productRoot(el);
        var host = hostFor(el);
        if (!root || !host)
            return;

        var tray = trayFor(host);
        var news = [];
        var discounts = [];

        root.querySelectorAll(BADGE_SELECTOR).forEach(function (badge) {
            if (productRoot(badge) !== root)
                return;
            if (badge.classList.contains('new-product-badge'))
                news.push(badge);
            else
                discounts.push(badge);
        });

        news.concat(discounts).forEach(function (badge) {
            tray.appendChild(badge);
            badge.classList.add('is-overlay');
            badge.classList.add('in-badge-tray');
        });
    }

    function placeAll() {
        document.querySelectorAll(BADGE_SELECTOR).forEach(placeIntoTray);
    }

    if (document.readyState === 'loading')
        document.addEventListener('DOMContentLoaded', placeAll);
    else
        placeAll();
})();
