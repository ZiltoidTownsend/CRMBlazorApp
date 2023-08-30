class CustomScrollListener {

    constructor() {
        this.throttleScrollHandlerId = -1;
        //needed as variable to remove the event listeners
        this.handlerRef = null;
    }

    // subscribe to throttled scroll event
    listenForScroll(dotnetReference, selector) {
        //if selector is null, attach to document
        let element = selector
            ? document.querySelector(selector)
            : document;

        this.handlerRef = this.throttleScrollHandler.bind(this, dotnetReference);
        // add the event listener
        element.addEventListener(
            'scroll',
            this.handlerRef,
            false
        );
    }

    // fire the event just once each 100 ms, **it's hardcoded**
    throttleScrollHandler(dotnetReference, event) {
        clearTimeout(this.throttleScrollHandlerId);

        this.throttleScrollHandlerId = window.setTimeout(
            this.scrollHandler.bind(this, dotnetReference, event),
            100
        );
    }

    // when scroll event is fired, pass this information to
    // the RaiseOnScroll C# method of the ScrollListener
    // We pass the scroll coordinates of the element and
    // the boundingClientRect of the first child, because
    // scrollTop of body is always 0. With this information,
    // we can trigger C# events on different scroll situations
    scrollHandler(dotnetReference, event) {
        try {
            let element = event.target;

            //data to pass
            let scrollTop = element.scrollTop;
            let scrollHeight = element.scrollHeight;
            let scrollWidth = element.scrollWidth;
            let scrollLeft = element.scrollLeft;
            let nodeName = element.nodeName;

            //data to pass
            let firstChild = element.firstElementChild;
            let firstChildBoundingClientRect = firstChild.getBoundingClientRect();

            const height = document.body.offsetHeight
            const screenHeight = window.innerHeight

            // Они могут отличаться: если на странице много контента,
            // высота документа будет больше высоты экрана (отсюда и скролл).

            // Записываем, сколько пикселей пользователь уже проскроллил:
            const scrolled = window.scrollY

            // Обозначим порог, по приближении к которому
            // будем вызывать какое-то действие.
            // В нашем случае — четверть экрана до конца страницы:
            const threshold = height - screenHeight / 4

            // Отслеживаем, где находится низ экрана относительно страницы:
            const position = scrolled + screenHeight

            if (position >= threshold) {
                // Если мы пересекли полосу-порог, вызываем нужное действие.
                //invoke C# method
                dotnetReference.invokeMethodAsync('CustomScroll', {
                    firstChildBoundingClientRect,
                    scrollLeft,
                    scrollTop,
                    scrollHeight,
                    scrollWidth,
                    nodeName,
                });
            }

        } catch (error) {
            console.log('[MudBlazor] Error in scrollHandler:', { error });
        }
    }

    //remove event listener
    cancelListener(selector) {
        let element = selector
            ? document.querySelector(selector)
            : document;

        element.removeEventListener('scroll', this.handlerRef);
    }
};
window.customScrollListener = new CustomScrollListener();