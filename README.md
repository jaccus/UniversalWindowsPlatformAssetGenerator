# Universal Windows Platform Asset Generator

Goal
----

The goal of this project is to generate all necessary assets for a Universal Windows app targeting Windows 10 devices.

Origin of the code base
-----------------------

Code base has originally been forked from https://wpiconmaker.codeplex.com/ WPF app which, given a png/jpg image, allows to crop it, and produces png icons in sizes:
- 200x200px (logo_200.png),
- 173x173px (logo_173.png, Background.png),
- 99x99px (logo_99.png),
- 62x62px (logo_62.png, ApplicationIcon.png).

That's a total of 4 different icon assets.

Minimum viable product - target
-------------------------------

Minimum requirement for target application is producing icon assets in sizes:
- Square 71x71px Logo,
- Square 150x150px Logo,
- Wide 310x150px Logo,
- Square 310x310px Logo,
- Square 44x44px Logo,
- Store Logo 50x50px,
- Badge Logo 24x24px,
- Splash Screen 620x300px.

Moreover, all asset icons should have their scaled up versions for:
- 100%,
- 125%,
- 150%,
- 200%,
- 400%.

That's a total of 40 different icon assets.

Contributions
-------------

All contributions are very much welcomed, please submit your great PRs.
